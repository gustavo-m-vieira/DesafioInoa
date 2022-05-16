using System.Configuration;

namespace DesafioInoa.utils
{
	public class EmailHandler
	{
		
		private float sellPrice;
		private float buyPrice;
		private string ticker;

		public EmailHandler(string ticker, float sellPrice, float buyPrice)
		{
			var settings = ConfigurationManager.AppSettings;

			this.sellPrice = sellPrice;
			this.buyPrice = buyPrice;
			// this.emailToNotify = settings["to"];
			// this.emailFrom = settings["from"];
			// this.emailPassword = settings["password"];
		}

		private void sendMail(string subject, string message)
        {

        }

		public void notifyToSell(float currentPrice)
        {
			string subject = "Alert! Time to sell!";
			string message = "The ticker" + this.ticker + "has riched the selling point " + this.sellPrice + "! It's price is " + currentPrice;

			sendMail(subject, message);
        }

		public void notifyToBuy(float currentPrice)
        {
			string subject = "Alert! Time to buy!";
			string message = "The ticker" + this.ticker + "has riched the buying point " + this.buyPrice + "! It's price is " + currentPrice;

			sendMail(subject, message);
		}
	}
}

