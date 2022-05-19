using System;
using System.Globalization;
using DesafioInoa.utils;

namespace DesafioInoa
{
    class Program
    {
        public static async Task<int> Main(string[] args)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("en-US");

            (string ticker, float sellPrice, float buyPrice) = parseArgs(args);

            EmailHandler emailHandler = new EmailHandler(ticker, sellPrice, buyPrice);
            TickerHandler tickerHandler = new TickerHandler(ticker);

            Console.WriteLine(ticker);
            Console.WriteLine(sellPrice);
            Console.WriteLine(buyPrice);

            float tickerCurrentPrice = await tickerHandler.getPriceAsync();
            float tickerCurrentPrice2 = await tickerHandler.getPriceAsync();

            Console.WriteLine(tickerCurrentPrice + "      " + tickerCurrentPrice2);

            return 0;
        }

        static Tuple<string, float, float> parseArgs(string[] args)
        {
            if (args.Length != 3)
            {
                throw new ArgumentException("You must pass ticker, price to sell and price to buy!");
            }

            string ticker = args[0];
            float sellPrice;
            float buyPrice;

            if (!float.TryParse(args[1], out sellPrice)) throw new ArgumentException("sellPrice must be a float value!");
            if (!float.TryParse(args[2], out buyPrice)) throw new ArgumentException("buyPrice must be a float value!");

            return Tuple.Create(ticker, sellPrice, buyPrice);
        }

    }
}