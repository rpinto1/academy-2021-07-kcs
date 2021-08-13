using System;

namespace Pedro
{
    class Program
    {
        static void Main(string[] args)
        {
            string ticker;

            ticker = "MSFT:US";
            Console.WriteLine("Rule #1 method for " + ticker + "\n");
            Console.WriteLine("\tScore is = " + (Rule1.GetScore(ticker) * 100).ToString("n2") + " %");
            Console.WriteLine("\tSticker Price is: " + Rule1.GetStickerPrice(ticker).ToString("n2"));
            Console.WriteLine("\tMargin of Safety is: " + Rule1.GetMarginOfSafety(ticker).ToString("n2"));
            Console.WriteLine("\n\n\n");

            ticker = "AMZN:US";
            Console.WriteLine("Rule #1 method for " + ticker + "\n");
            Console.WriteLine("\tScore is = " + (Rule1.GetScore(ticker) * 100).ToString("n2") + " %");
            Console.WriteLine("\tSticker Price is: " + Rule1.GetStickerPrice(ticker).ToString("n2"));
            Console.WriteLine("\tMargin of Safety is: " + Rule1.GetMarginOfSafety(ticker).ToString("n2"));
            Console.WriteLine("\n\n\n");

            ticker = "AAPL:US";
            Console.WriteLine("Rule #1 method for " + ticker + "\n");
            Console.WriteLine("\tScore is = " + (Rule1.GetScore(ticker) * 100).ToString("n2") + " %");
            Console.WriteLine("\tSticker Price is: " + Rule1.GetStickerPrice(ticker).ToString("n2"));
            Console.WriteLine("\tMargin of Safety is: " + Rule1.GetMarginOfSafety(ticker).ToString("n2"));
            Console.WriteLine("\n\n\n");

            ticker = "BAC:US";
            Console.WriteLine("Rule #1 method for " + ticker + "\n");
            Console.WriteLine("\tScore is = " + (Rule1.GetScore(ticker) * 100).ToString("n2") + " %");
            Console.WriteLine("\tSticker Price is: " + Rule1.GetStickerPrice(ticker).ToString("n2"));
            Console.WriteLine("\tMargin of Safety is: " + Rule1.GetMarginOfSafety(ticker).ToString("n2"));
            Console.WriteLine("\n\n\n");

            ticker = "FB:US";
            Console.WriteLine("Rule #1 method for " + ticker + "\n");
            Console.WriteLine("\tScore is = " + (Rule1.GetScore(ticker) * 100).ToString("n2") + " %");
            Console.WriteLine("\tSticker Price is: " + Rule1.GetStickerPrice(ticker).ToString("n2"));
            Console.WriteLine("\tMargin of Safety is: " + Rule1.GetMarginOfSafety(ticker).ToString("n2"));
            Console.WriteLine("\n\n\n\n");
        }
    }
}
