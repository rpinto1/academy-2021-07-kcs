using System;

namespace Pedro
{
    class Program
    {
        static void Main(string[] args)
        {
            double scoreMSFT = Rule1.GetScore("MSFT:US");
            Console.WriteLine("Rule #1 score for MSFT:US is = " + (scoreMSFT * 100).ToString("n2") + " %\n\n");

            double scoreAMZN = Rule1.GetScore("AMZN:US");
            Console.WriteLine("Rule #1 score for AMZN:US is = " + (scoreAMZN * 100).ToString("n2") + " %\n\n");

            double scoreAAPL = Rule1.GetScore("AAPL:US");
            Console.WriteLine("Rule #1 score for AAPL:US is = " + (scoreAAPL * 100).ToString("n2") + " %\n\n");

            double scoreBAC = Rule1.GetScore("BAC:US");
            Console.WriteLine("Rule #1 score for BAC:US is = " + (scoreBAC * 100).ToString("n2") + " %\n\n");

            double scoreFB = Rule1.GetScore("FB:US");
            Console.WriteLine("Rule #1 score for FB:US is = " + (scoreFB * 100).ToString("n2") + " %\n\n");

            Console.WriteLine("\n\n");
        }
    }
}
