using KCSit.SalesforceAcademy.Lasagna.Business;
using KCSit.SalesforceAcademy.Lasagna.Business.Interfaces;
using KCSit.SalesforceAcademy.Lasagna.Data;
using KCSit.SalesforceAcademy.Lasagna.Data.Pocos;
using KCSit.SalesforceAcademy.Lasagna.DataAccess;
using KCSit.SalesforceAcademy.Lasagna.DataAccess.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web;

namespace KCSit.SalesforceAcademy.Lasagna.Business
{
    public class Rule1BO : IRule1BO
    {
        // Indicator weight in the final score calculation
        private const decimal roicWeight = 0.30M;
        private const decimal equityWeight = 0.25M;
        private const decimal epsWeight = 0.20M;
        private const decimal salesWeight = 0.15M;
        private const decimal cashWeight = 0.10M;

        // Indicator threshold according to Rule #1 method
        private const decimal roicTreshold = 0.10M;
        private const decimal equityTreshold = 0.10M;
        private const decimal epsTreshold = 0.10M;
        private const decimal salesTreshold = 0.10M;
        private const decimal cashTreshold = 0.10M;

        // Variables needed for Sticker Price
        private const decimal rate = 0.15M;
        private const int nper = 10;

        private readonly IGenericDAO _genericDao;
        private readonly ISearchDAO _searchDAO;
        private readonly IRule1DAO _rule1DAO;
        private readonly IGenericBusinessLogic _genericBusinessLogic;

        public Rule1BO(IGenericDAO genericDao, ISearchDAO searchDAO, IRule1DAO rule1DAO, IGenericBusinessLogic genericBusinessLogic)
        {
            _genericDao = genericDao;
            _searchDAO = searchDAO;
            _rule1DAO = rule1DAO;
            _genericBusinessLogic = genericBusinessLogic;
        }



        public async Task<GenericReturn> GetRule1Info(string queryString)
        {
            return await _genericBusinessLogic.GenericTransaction(async () =>
            {
                const int scoringMethod = 1;

                return await _rule1DAO.GetInfo(queryString, scoringMethod);
            });
        }



        public async Task<GenericReturn> UpdateOneScore(string tickerStr)
        {
            return await _genericBusinessLogic.GenericTransaction(async () =>
            {
                const int scoringMethod = 1;

                var ticker = JsonSerializer.Deserialize<string>(tickerStr);

                var companyId = _searchDAO.Get(ticker);

                var KeyRatiosList = (await _rule1DAO.GetKeyRatios(ticker)).ToList();

                var balanceSheetList = (await _rule1DAO.GetBalanceSheet(ticker)).ToList();

                var incomeStatementList = (await _rule1DAO.GetIncomeStatement(ticker)).ToList();

                var roic = (from kr in KeyRatiosList
                            select kr.Roic).ToList();

                var equity = (from bs in balanceSheetList
                              select bs.Equity).ToList();

                var eps = (from incStat in incomeStatementList
                           select incStat.Eps).ToList();

                var sales = (from incStat in incomeStatementList
                             select incStat.Sales).ToList();

                var cash = (from bs in balanceSheetList
                            select bs.Cash).ToList();

                var score = CalculateScore(roic, equity, eps, sales, cash);



                var PriceToEarnings = (from kr in KeyRatiosList
                                       where kr.Ticker.Equals(ticker)
                                       select kr.PriceToEarnings).ToList();

                var stickerPrice = CalculateStickerPrice(PriceToEarnings.ElementAt(0), eps.ElementAt(0));

                var marginOfSafety = stickerPrice / 2;


                var scoreInfo = await _rule1DAO.GetScoreId(ticker, 1);

                var newScore = new Score
                {
                    Id = scoreInfo?.ScoreId == null ? 0 : scoreInfo.ScoreId,
                    ScoringMethodId = scoringMethod,
                    CompanyId = companyId,
                    Score1 = (double)(Math.Truncate(score * 100) / 100),
                    StickerPrice = Math.Truncate(stickerPrice * 100) / 100,
                    MarginOfSafety = Math.Truncate(marginOfSafety * 100) / 100,
                    Uuid = Guid.NewGuid(),
                    UpdatedOn = DateTime.Parse(String.Format("{0:G}", DateTime.Now)),
                };

                _genericDao.Update<Score>(newScore);


                Console.WriteLine("\tScore: " + score.ToString("n2") + "      Sticker Price: " + stickerPrice + "\n");

            });
        }

        public async Task<GenericReturn<List<string>>> UpdateManyScores(string tickersQueryStr)
        {
            //return await _genericBusinessLogic.GenericTransaction(async () =>
            //{

            const int scoringMethod = 1;

            var tickerStr = HttpUtility.ParseQueryString(tickersQueryStr).Get("tickers");

            var tickers = JsonSerializer.Deserialize<string[]>(tickerStr);

            var bulkSize = 200;

            var result = new List<string>();

            for (int updatedTickersCounter = 0; updatedTickersCounter < tickers.Length; updatedTickersCounter += bulkSize)
            {
                var tickersBulk = tickers.Skip(updatedTickersCounter).Take(bulkSize);

                var updatedTickers = await UpdateScoresByBulk(tickersBulk.ToList(), scoringMethod);

                result.AddRange(updatedTickers);
            }

            return new GenericReturn<List<string>> { Succeeded = true, Result = result };
            //});
        }

        public async Task<GenericReturn> UpdateAllScores()
        {
            //return await _genericBusinessLogic.GenericTransaction(async () =>
            //{
            const int scoringMethod = 1;

            var bulkSize = 200;

            var companiesCount = await _genericDao.GetCount<Company>();

            for (int updatedCompaniesCounter = 0; updatedCompaniesCounter < companiesCount; updatedCompaniesCounter += bulkSize)
            {
                var tickersBulk = await _rule1DAO.GetTickersByBulk(updatedCompaniesCounter, bulkSize);

                await UpdateScoresByBulk(tickersBulk.ToList(), scoringMethod);
            }
            //});
            return new GenericReturn { Succeeded = true, Message = "ok" };
        }

        private async Task<List<string>> UpdateScoresByBulk(List<string> tickersBulk, int scoringMethod)
        {
            var companiesBulk = await _rule1DAO.GetCompaniesByTickerList(tickersBulk);

            var keyRatiosByBulk = await _rule1DAO.GetKeyRatiosByBulk(tickersBulk);

            var balanceSheetByBulk = await _rule1DAO.GetBalanceSheetByBulk(tickersBulk);

            var incomeStatementByBulk = await _rule1DAO.GetIncomeStatementByBulk(tickersBulk);

            var ScoreByBulk = await _rule1DAO.GetScoreByBulk(tickersBulk, scoringMethod);

            var scoresResult = new List<Score>();

            foreach (CompanyPoco company in companiesBulk)
            {
                var roic = (from kr in keyRatiosByBulk
                            where kr.Ticker.Equals(company.Ticker)
                            select kr.Roic).ToList();

                var equity = (from bs in balanceSheetByBulk
                              where bs.Ticker.Equals(company.Ticker)
                              select bs.Equity).ToList();

                var eps = (from incStat in incomeStatementByBulk
                           where incStat.Ticker.Equals(company.Ticker)
                           select incStat.Eps).ToList();

                var sales = (from incStat in incomeStatementByBulk
                             where incStat.Ticker.Equals(company.Ticker)
                             select incStat.Sales).ToList();

                var cash = (from bs in balanceSheetByBulk
                            where bs.Ticker.Equals(company.Ticker)
                            select bs.Cash).ToList();

                // Clean all values between -0.01 to 0.01 to zero to avoid big numbers after divisions
                const decimal treshold = 0.01M;
                var filteredRoic = roic.Select(r => Math.Abs(r) <= treshold ? 0 : r).ToList();
                var filteredEquity = equity.Select(r => Math.Abs(r/1000000) <= treshold ? 0 : r).ToList();  // value displayed in Millions
                var filteredEps = eps.Select(r => Math.Abs(r) <= treshold ? 0 : r).ToList();
                var filteredSales = sales.Select(r => Math.Abs(r) <= treshold ? 0 : r).ToList();
                var filteredCash = cash.Select(r => Math.Abs(r) <= treshold ? 0 : r).ToList();

                const int minSamples = 5;
                var score = (filteredRoic.Count >= minSamples && filteredEquity.Count >= minSamples && filteredEps.Count >= minSamples) ? 
                    CalculateScore(filteredRoic, filteredEquity, filteredEps, filteredSales, filteredCash) : 0;

                var PriceToEarnings = (from kr in keyRatiosByBulk
                                       where kr.Ticker.Equals(company.Ticker)
                                       select kr.PriceToEarnings).ToList();

                var stickerPrice = CalculateStickerPrice(PriceToEarnings.ElementAt(0), eps.ElementAt(0));

                var marginOfSafety = stickerPrice / 2;

                var scoreInfo = ScoreByBulk.Where(s => s.Ticker == company.Ticker).SingleOrDefault();

                scoresResult.Add(new Score
                {
                    Id = scoreInfo?.ScoreId == null ? 0 : scoreInfo.ScoreId,
                    ScoringMethodId = scoringMethod,
                    CompanyId = company.Id,
                    Score1 = (double)(Math.Truncate(score * 100) / 100),
                    StickerPrice = Math.Truncate(stickerPrice * 100) / 100,
                    MarginOfSafety = Math.Truncate(marginOfSafety * 100) / 100,
                    Uuid = Guid.NewGuid(),
                    UpdatedOn = DateTime.Parse(String.Format("{0:G}", DateTime.Now)),
                });
            }

            await _genericBusinessLogic.GenericTransaction(async () =>
            {
                _genericDao.UpdateRange<Score>(scoresResult);
            });

            return tickersBulk;
        }







        private decimal CalculateScore(List<decimal> roic, List<decimal> equity, List<decimal> eps, List<decimal> sales, List<decimal> cash)
        {
            // Calculate the growth rate for 4 indicators (the roic indicator is already a growth rate itsef...)
            var equityDelta = CalculateDelta(equity);
            var epsDelta = CalculateDelta(eps);
            var salesDelta = CalculateDelta(sales);
            var cashDelta = CalculateDelta(cash);

            // Calculate the weight for each of the 20 years
            var roicYearWeight = CalculateYearWeights(roic);
            var equityYearWeight = CalculateYearWeights(equityDelta);
            var epsYearWeight = CalculateYearWeights(epsDelta);
            var salesYearWeight = CalculateYearWeights(salesDelta);
            var cashYearWeight = CalculateYearWeights(cashDelta);

            // Calculate the weighted aggregate for the 5 indicators for the last 20 years
            var roicAggregate = CalculateAggregate(roic, roicYearWeight);
            var equityAggregate = CalculateAggregate(equityDelta, equityYearWeight);
            var epsAggregate = CalculateAggregate(epsDelta, epsYearWeight);
            var salesAggregate = CalculateAggregate(salesDelta, salesYearWeight);
            var cashAggregate = CalculateAggregate(cashDelta, cashYearWeight);

            // Calculate the standard deviation for each indicator
            var roicStd = CalculateStdDeviation(roic);
            var equityStd = CalculateStdDeviation(equityDelta);
            var epsStd = CalculateStdDeviation(epsDelta);
            var salesStd = CalculateStdDeviation(salesDelta);
            var cashStd = CalculateStdDeviation(cashDelta);

            // Calculate the partial score for each indicator
            var roicPartialScore = CalculatePartialScores(roicAggregate, roicTreshold, roicWeight, roicStd);
            var equityPartialScore = CalculatePartialScores(equityAggregate, equityTreshold, equityWeight, equityStd);
            var epsPartialScore = CalculatePartialScores(epsAggregate, epsTreshold, epsWeight, epsStd);
            var salesPartialScore = CalculatePartialScores(salesAggregate, salesTreshold, salesWeight, salesStd);
            var cashPartialScore = CalculatePartialScores(cashAggregate, cashTreshold, cashWeight, cashStd);

            var score = (roicPartialScore + equityPartialScore + epsPartialScore + salesPartialScore + cashPartialScore) * 100;

            return score;
        }

        private List<decimal> CalculateDelta(List<decimal> indicator)
        {
            var treshold = 0.01M;

            var result = new List<decimal>();

            for (int i = 0; i < indicator.Count - 1; i++)
            {
                var final = indicator[i];
                var initial = indicator[i + 1];

                if (initial < treshold)
                {
                    result.Add(0);
                    continue;
                }

                var delta = ((final - initial) / initial) * Math.Sign(initial);

                result.Add(delta);
            }

            return result;
        }

        private List<decimal> CalculateYearWeights(List<decimal> indicator)
        {
            decimal logSum = 0;
            const decimal logOffset = 1.5M;   // offset for different logarithmic curve

            var result = new List<decimal>();

            for (int i = 0; i < indicator.Count; i++)
            {
                if (indicator[i] == 0)
                {
                    result.Add(0);
                    continue;
                }

                result.Add(logOffset - (decimal)Math.Log10(i + 1));

                logSum += result[i];
            }

            if (logSum != 0)
            {
                //normalize weights to achieve a total log sum of 100 %
                for (int i = 0; i < result.Count; i++)
                {
                    result[i] /= logSum;
                }
            }

            return result;
        }

        private decimal CalculateAggregate(List<decimal> indicator, List<decimal> weight)
        {
            decimal result = 0;

            for (int i = 0; i < weight.Count; i++)
            {
                result += indicator[i] * weight[i];
            }

            return result;
        }

        private decimal CalculateStdDeviation(List<decimal> indicator)
        {
            double result = 0;

            var indicatorDouble = new List<double>();

            foreach (decimal d in indicator)
            {
                indicatorDouble.Add((double)d);
            }


            if (indicator.Any())
            {
                double average = indicatorDouble.Average();
                double sum = indicatorDouble.Sum(d => Math.Pow(d - average, 2));
                result = Math.Sqrt((sum) / (indicatorDouble.Count()));
            }

            if(result == 0)
            {
                result = 1; // this is to avoid subsequent division by zero
            }

            return (decimal)result;
        }


        private decimal CalculatePartialScores(decimal aggregate, decimal treshold, decimal weight, decimal std)
        {
            var result = aggregate / treshold * weight;

            if (std != 0)
            {
                result /= std;
            }

            return result;
        }


        private decimal CalculateStickerPrice(decimal pe, decimal epsFY0)
        {
            decimal feps = epsFY0 * (decimal)(Math.Pow(((double)(1 + rate)), (double)nper));

            decimal fv = feps * pe;

            return fv / (decimal)(Math.Pow((double)(1 + rate), (double)nper));
        }



    }
}
