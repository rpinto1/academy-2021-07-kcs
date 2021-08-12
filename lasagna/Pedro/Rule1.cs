using System;
using System.Collections.Generic;
using System.Text;

namespace Pedro
{
    class Rule1
    {
        // Indicator weight in the final score calculation
        const double roicWeight = 0.30;
        const double equityWeight = 0.25;
        const double epsWeight = 0.20;
        const double salesWeight = 0.15;
        const double cashWeight = 0.10;

        // Indicator threshold according to Rule #1 method
        const double roicTreshold = 0.10;
        const double equityTreshold = 0.10;
        const double epsTreshold = 0.10;
        const double salesTreshold = 0.10;
        const double cashTreshold = 0.10;

        static double[] roic, equity, eps, sales, cash;
        static double[] deltaEquity, deltaEps, deltaSales, deltaCash;
        static double[] yearWeigth = new double[20];
        static double roicAggregate, equityAggregate, epsAggregate, salesAggregate, cashAggregate;
        static double roicPartialScore, equityPartialScore, epsPartialScore, salesPartialScore, cashPartialScore;

        public static double GetScore(string ticker)
        {
            //------------------------------------------------------------------------------------------------------------------------------------------
            //-------------------------------- MOCKS for testing only ----------------------------------------------------------------------------------
            GetDataFromMocks(ticker);
            //-------------------------------- MOCKS for testing only ----------------------------------------------------------------------------------
            //------------------------------------------------------------------------------------------------------------------------------------------

            //GetDataFromBD(ticker);    // ToDo...


            // Calculate the growth rate for 4 indicators (the roic indicator is already a growth rate itsef...)
            deltaEquity = CalculateDelta(equity);
            deltaEps = CalculateDelta(eps);
            deltaSales = CalculateDelta(sales);
            deltaCash = CalculateDelta(cash);

            // Calculate the weight for each of the 20 years
            CalculateYearWeights();

            // Calculate the weighted aggregate for the 5 indicators for the last 20 years
            roicAggregate = CalculateAggregate(roic);
            equityAggregate = CalculateAggregate(deltaEquity);
            epsAggregate = CalculateAggregate(deltaEps);
            salesAggregate = CalculateAggregate(deltaSales);
            cashAggregate = CalculateAggregate(deltaCash);

            // Calculate the partial score for each indicator
            CalculatePartialScores();

            //---------- Print to Screen for debugging ------------------------------
            PrintToScreen(false);

            return roicPartialScore + equityPartialScore + epsPartialScore + salesPartialScore + cashPartialScore;
        }


        private static void GetDataFromMocks(string ticker)
        {
            switch (ticker)
            {
                case ("MSFT:US"):
                    roic = new double[20] { 0.3071, 0.2394, 0.2273, 0.0964, 0.1673, 0.1706, 0.107, 0.2133, 0.253, 0.2305, 0.3823, 0.3851, 0.3571, 0.5248, 0.3951, 0.2856, 0.1993, 0.1169, 0.1286, 0.1574 };
                    equity = new double[20] { 0.1866, 0.154, 0.132, 0.1061, 0.112, 0.0899, 0.097, 0.1069, 0.0932, 0.078, 0.0664, 0.0517, 0.044, 0.0383, 0.0315, 0.0381, 0.0441, 0.0687, 0.0597, 0.047 };
                    eps = new double[20] { 8.12, 5.82, 5.11, 2.15, 3.29, 2.59, 1.49, 2.66, 2.61, 2.02, 2.73, 2.13, 1.63, 1.9, 1.44, 1.21, 1.13, 0.76, 0.7, 0.72 };
                    sales = new double[20] { 168088, 143015, 125843, 110360, 96571, 91154, 93580, 86833, 77849, 73723, 69943, 62484, 58437, 60420, 51122, 44282, 39788, 36835, 32187, 28365 };
                    cash = new double[20] { 56118, 45234, 38260, 32252, 31378, 24982, 23724, 27017, 24576, 29321, 24639, 22096, 15918, 18430, 15532, 12826, 15793, 13517, 14906, 13739 };
                    break;

                case ("AMZN:US"):
                    roic = new double[20] { 0.1408, 0.1148, 0.1415, 0.0606, 0.0764, 0.0235, -0.0126, 0.0203, -0.0037, 0.0757, 0.1770, 0.2070, 0.2238, 0.2237, 0.1108, 0.2136, 0.4623, 0.0381, -0.1787, -0.5947 };
                    equity = new double[20] { 1.8315, 1.2313, 0.871, 0.5620, 0.3985, 0.2806, 0.2325, 0.2096, 0.1808, 0.1683, 0.1505, 0.1189, 0.0619, 0.0282, 0.0102, 0.0058, -0.0053, -0.0247, -0.0358, -0.0395 };
                    eps = new double[20] { 0.4264, 0.2346, 0.2068, 0.0632, 0.0501, 0.0128, -0.0052, 0.006, -0.0009, 0.0139, 0.0258, 0.0208, 0.0152, 0.0115, 0.0046, 0.0087, 0.0145, 0.0009, -0.0039, -0.0156 };
                    sales = new double[20] { 386064, 280522, 232887, 177866, 135987, 107006, 88988, 74452, 61093, 48077, 34204, 24509, 19166, 14835, 10711, 8490, 6921, 5263.699, 3932.936, 3122.433 };
                    cash = new double[20] { 31020, 25825, 19400, 8307, 10466, 7450, 1949, 2031, 395, 2092, 2516, 2920, 1364, 1181, 486, 529, 477, 351, 135, -170 };
                    break;

                case ("AAPL:US"):
                    roic = new double[20] { 0.3051, 0.263, 0.2526, 0.208, 0.229, 0.3231, 0.275, 0.2863, 0.4284, 0.4167, 0.3528, 0.3054, 0.3323, 0.2851, 0.2285, 0.2124, 0.0554, 0.0154, 0.015, -0.0058 };
                    equity = new double[20] { 0.0373, 0.0487, 0.0536, 0.0638, 0.0583, 0.0515, 0.0455, 0.0474, 0.0447, 0.0292, 0.0185, 0.0125, 0.0088, 0.0058, 0.0041, 0.0031, 0.0023, 0.0021, 0.002, 0.002 };
                    eps = new double[20] { 0.0331, 0.0299, 0.03, 0.0232, 0.0209, 0.0232, 0.0162, 0.0143, 0.016, 0.01, 0.0055, 0.0033, 0.0025, 0.0014, 0.0008, 0.0006, 0.0001, 0, 0, 0 };
                    sales = new double[20] { 274515, 260174, 265595, 229234, 215639, 233715, 182795, 170910, 156508, 108249, 65225, 42905, 37491, 24578, 19315, 13931, 8279, 6207, 5742, 5363 };
                    cash = new double[20] { 73365, 58896, 64121, 51774, 53497, 70019, 50142, 45501, 42561, 33269, 16590, 9015, 8505, 4735, 1563, 2275, 758, 125, -85, -47 };
                    break;

                case ("BAC:US"):
                    roic = new double[20] { 0.033, 0.0525, 0.054, 0.0353, 0.0347, 0.0306, 0.0106, 0.0213, 0.0071, 0.0021, -0.003, 0.0093, 0.007, 0.0313, 0.057, 0.0553, 0.0631, 0.0701, 0.0657, 0.0451 };
                    equity = new double[20] { 0.2824, 0.2557, 0.2374, 0.2271, 0.2181, 0.2082, 0.2118, 0.1909, 0.2013, 0.2064, 0.2162, 0.2513, 0.3032, 0.319, 0.2881, 0.2489, 0.2599, 0.1582, 0.1798, 0.1492 };
                    eps = new double[20] { 0.0188, 0.0277, 0.0264, 0.0163, 0.0157, 0.0138, 0.0043, 0.0094, 0.0026, 0.0001, -0.0037, -0.0029, 0.0054, 0.0332, 0.0466, 0.041, 0.0371, 0.0364, 0.0304, 0.0213 };
                    sales = new double[20] { 86266, 91244, 91020, 87126, 83701, 82965, 85894, 86090, 83351, 86094, 104960, 109643, 73897, 62769, 69587, 54963, 49665, 38827, 35124, 35113 };
                    cash = new double[20] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0};
                    break;

                case ("FB:US"):
                    roic = new double[20] { 0.2343, 0.1891, 0.2782, 0.2386, 0.197, 0.0914, 0.1132, 0.1019, 0.0056, 0.2556, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
                    equity = new double[20] { 0.4442, 0.3514, 0.288, 0.2515, 0.2024, 0.155, 0.1355, 0.0615, 0.0543, 0.0181, 0.0066, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
                    eps = new double[20] { 0.1022, 0.0648, 0.0765, 0.0549, 0.0356, 0.0131, 0.0112, 0.0062, 0.0002, 0.0031, 0.0018, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
                    sales = new double[20] { 85965, 70697, 55838, 40653, 27638, 17928, 12466, 7872, 5089, 3711, 1974, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
                    cash = new double[20] { 23632, 21212, 15359, 17483, 11617, 7797, 5495, 2860, 377, 943, 405, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
                    break;

                default:
                    break;
            }
        }

        private static void CalculatePartialScores()
        {
            roicPartialScore = roicAggregate / roicTreshold * roicWeight;

            equityPartialScore = equityAggregate / equityTreshold * equityWeight;

            epsPartialScore = epsAggregate / epsTreshold * epsWeight;

            salesPartialScore = salesAggregate / salesTreshold * salesWeight;

            cashPartialScore = cashAggregate / cashTreshold * cashWeight;
        }

        private static double CalculateAggregate(double[] indicator)
        {
            double result = 0;

            for (int i = 0; i < indicator.Length; i++)
            {
                result += indicator[i] * yearWeigth[i];
            }

            return result;
        }

        private static void CalculateYearWeights()
        {
            double logSum = 0;
            const double logOffset = 1.5;   // offset for different logarithmic curve

            for (int i = 0; i < roic.Length; i++)
            {
                if (deltaEquity[i] == 0 )
                    {
                        yearWeigth[i] = 0;
                    continue;
                }

                yearWeigth[i] = logOffset - Math.Log10(i + 1);

                logSum += yearWeigth[i];
            }

            //normalize weights to achieve a total log sum of 100 %
            for (int i = 0; i < yearWeigth.Length; i++)
            {
                yearWeigth[i] /= logSum;
            }
        }

        private static double[] CalculateDelta(double[] indicator)
        {
            double[] result = new double[20];

            for (int i = 0; i < indicator.Length - 1; i++)
            {
                result[i] = indicator[i + 1] == 0 ? 0 : (indicator[i] - indicator[i + 1]) / indicator[i + 1];
            }

            return result;
        }

        private static void PrintArray(string desc, double[] array)
        {
            Console.WriteLine(desc);

            foreach (var item in array)
            {
                Console.Write((item * 100).ToString("n2") + "%, ");
            }

            Console.WriteLine("\n\n");
        }

        private static void PrintToScreen(bool print)
        {
            if (print)
            {
                PrintArray("Delta Equity", deltaEquity);
                PrintArray("Delta EPS", deltaEps);
                PrintArray("Delta Sales", deltaSales);
                PrintArray("Delta Cash", deltaCash);
                PrintArray("Year Weights", yearWeigth);

                Console.WriteLine("Roic Aggregate: " + (roicAggregate * 100).ToString("n2"));
                Console.WriteLine("Equity Aggregate: " + (equityAggregate * 100).ToString("n2"));
                Console.WriteLine("EPS Aggregate: " + (epsAggregate * 100).ToString("n2"));
                Console.WriteLine("Sales Aggregate: " + (salesAggregate * 100).ToString("n2"));
                Console.WriteLine("Cash Aggregate: " + (cashAggregate * 100).ToString("n2"));
                Console.WriteLine("");

                Console.WriteLine("Roic Partial Score: " + (roicPartialScore * 100).ToString("n2"));
                Console.WriteLine("Equity Partial Score: " + (equityPartialScore * 100).ToString("n2"));
                Console.WriteLine("Eps Partial Score: " + (epsPartialScore * 100).ToString("n2"));
                Console.WriteLine("Sales Partial Score: " + (salesPartialScore * 100).ToString("n2"));
                Console.WriteLine("Cash Partial Score: " + (cashPartialScore * 100).ToString("n2"));
                Console.WriteLine("");
            }
        }

    }
}
