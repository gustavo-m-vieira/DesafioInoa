using Newtonsoft.Json;
using DesafioInoa.utils;

namespace DesafioInoa.utils
{
	public class TickerHandler
	{
		private readonly string ticker;
		private readonly string url = "https://api.hgbrasil.com/finance/stock_price?key=c3348c1a&symbol=";
		private readonly string invalidTickerErrorMessage = "Invalid ticker!";
		private string? lastUpdatedAt;

		public TickerHandler(string ticker)
		{
			this.ticker = ticker;
		}

		public async Task<float> getPriceAsync()
        {
			using (HttpClient httpClient = new())
            {
				try
                {
					string responseBody = await httpClient.GetStringAsync(url + this.ticker);

					ApiResponse apiResponse = JsonConvert.DeserializeObject<ApiResponse>(responseBody);

					if (apiResponse != null && apiResponse.valid_key == true)
                    {
						if (lastUpdatedAt != null && apiResponse.results[ticker].updated_at == lastUpdatedAt)
                        {
							// Didn't changed, so should not renotify
							return -1f;
                        }

						lastUpdatedAt = apiResponse.results[ticker].updated_at;
						return apiResponse.results[ticker].price;
                    }
						
					throw new ArgumentException(invalidTickerErrorMessage);

				} catch (Exception e)
                {
					if (e.Message == invalidTickerErrorMessage)
                    {
						// In case ticker is not a valid symbol, program should stop!
						throw e;
                    }

					Console.WriteLine("Failed to get price, trying again in 1 minute!");

					return -1f;
                }
				
            }
        }
	}
}

