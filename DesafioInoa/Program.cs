using System;
using System.Globalization;

namespace DesafioInoa
{
    class Program
    {
        static void Main(string[] args)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("en-US");

            (string ticker, float sellPrice, float buyPrice) = parseArgs(args);

            Console.WriteLine(ticker);
            Console.WriteLine(sellPrice);
            Console.WriteLine(buyPrice);
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