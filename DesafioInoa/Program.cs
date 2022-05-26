using DesafioInoa.utils;
using System.Globalization;

namespace DesafioInoa
{
    class Program
    {
        public static async Task<int> Main(string[] args)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("en-US");

            (string ticker, float sellPrice, float buyPrice, int interval) = parseArgs(args);

            await getPriceAndNotify(ticker, sellPrice, buyPrice, interval);

            return 0;
        }

        static Tuple<string, float, float, int> parseArgs(string[] args)
        {
            if (args.Length < 3)
            {
                throw new ArgumentException("You must pass ticker, price to sell and price to buy!");
            }

            string ticker = args[0];
            float sellPrice;
            float buyPrice;
            int interval = 5;

            if (!float.TryParse(args[1], out sellPrice)) throw new ArgumentException("sellPrice must be a float value!");
            if (!float.TryParse(args[2], out buyPrice)) throw new ArgumentException("buyPrice must be a float value!");
            if (args.Length > 4 && !int.TryParse(args[3], out interval)) throw new ArgumentException("interval must be a int value!");

            return Tuple.Create(ticker, sellPrice, buyPrice, interval);
        }

        static async Task getPriceAndNotify(string ticker, float sellPrice, float buyPrice, int interval)
        {            
            EmailHandler emailHandler = new(ticker, sellPrice, buyPrice);
            TickerHandler tickerHandler = new(ticker);

            Console.WriteLine("Starting to seek for " + ticker + " price in " + interval + " minutes interval.");

            while (true)
            {
                float tickerPrice = await tickerHandler.getPriceAsync();


                int currentInterval;
                if (tickerPrice != -1f)
                {
                    if (tickerPrice >= sellPrice)
                    {
                        emailHandler.notifyToSell(tickerPrice);
                    }
                    else if (tickerPrice <= buyPrice)
                    {
                        emailHandler.notifyToBuy(tickerPrice);
                    }
                    else
                    {
                        Console.WriteLine("Its not time to buy or sell.");
                    }

                    currentInterval = interval;
                }
                else
                {
                    Console.WriteLine("Failed to get price or price not updated yet");
                    currentInterval = 1; // 1 minute
                }

                await Task.Delay(currentInterval * 60000);
            }
        }
    }
}