using KCSit.SalesforceAcademy.Lasagna.Business;
using KCSit.SalesforceAcademy.Lasagna.Data;
using KCSit.SalesforceAcademy.Lasagna.Data.Pocos;
using KCSit.SalesforceAcademy.Lasagna.DataAccess;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KCSit.SalesforceAcademy.Lasagna.Rule1
{
    public class Rule1BO : IScore
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

        private readonly GenericDAO _genericDao;
        private readonly SearchDAO _searchDAO;
        private readonly GenericBusinessLogic _genericBusinessLogic;

        public Rule1BO(GenericDAO genericDao, SearchDAO searchDAO, GenericBusinessLogic genericBusinessLogic)
        {
            _genericDao = genericDao;
            _searchDAO = searchDAO;
            _genericBusinessLogic = genericBusinessLogic;
        }


        public async Task<GenericReturn> UpdateScore(string ticker)
        {
            return await _genericBusinessLogic.GenericTransaction(async () =>
            {
                Console.WriteLine(DateTime.Now + "   Ticker: " + ticker);

                var KeyRatiosList = _searchDAO.GetKeyRatios(ticker).ToList();

                var balanceSheetList = _searchDAO.GetBalanceSheet(ticker).ToList();

                var incomeStatementList = _searchDAO.GetIncomeStatement(ticker).ToList();

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



                var dailyInfo = _searchDAO.GetDailyInfo(ticker);

                var stickerPrice = CalculateStickerPrice(dailyInfo);

                var marginOfSafety = stickerPrice / 2;

                var companyId = _searchDAO.Get(ticker);


                var scoreInfo = _searchDAO.GetScore(ticker, 1);

                var newScore = new Score
                {
                    Id = scoreInfo?.ScoreId == null ? 0 : scoreInfo.ScoreId,
                    ScoringMethodId = 1,
                    CompanyId = companyId,
                    Score1 = (double)score,
                    StickerPrice = stickerPrice,
                    MarginOfSafety = marginOfSafety,
                    Uuid = Guid.NewGuid(),
                    UpdatedOn = DateTime.Now
                };

                _genericDao.Update<Score>(newScore);

                Console.WriteLine("\tScore: " + score.ToString("n2") + "      Sticker Price: " + stickerPrice + "\n");
            });
        }



        public async Task<GenericReturn> UpdateAllScores()
        {
            //return await _genericBusinessLogic.GenericTransaction(async () =>
            //{
                var bulkSize = 200;
                int updatedCompaniesCounter;

                var companiesCount = await _searchDAO.GetCompaniesCount();

                for (updatedCompaniesCounter = 0; updatedCompaniesCounter < companiesCount; updatedCompaniesCounter += bulkSize)
                {
                    Console.WriteLine("-----------------------------------------------------------------------------------");
                    Console.WriteLine("--------------------------------- Loop number: " + (updatedCompaniesCounter / bulkSize) + " ---------------------------------");
                    Console.WriteLine("-----------------------------------------------------------------------------------\n");

                    var companiesBulk = await _searchDAO.GetCompaniesByBulk(updatedCompaniesCounter, bulkSize);

                    List<string> tickersBulk = (from c in companiesBulk
                                                select c.Ticker).ToList();

                    var keyRatiosByBulk = _searchDAO.GetKeyRatiosByBulk(tickersBulk);

                    var balanceSheetByBulk = _searchDAO.GetBalanceSheetByBulk(tickersBulk);

                    var incomeStatementByBulk = _searchDAO.GetIncomeStatementByBulk(tickersBulk);

                    var dailyInfoByBulk = _searchDAO.GetDailyInfoByBulk(tickersBulk);

                    var ScoreByBulk = _searchDAO.GetScoreByBulk(tickersBulk, 1);

                    var scoresResult = new List<Score>();

                    foreach (CompanyPoco company in companiesBulk)
                    {

                        Console.WriteLine(DateTime.Now + "   Id: " + company.Id + "   Ticker: " + company.Ticker + "   Name: " + company.Name);

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

                        var score = CalculateScore(roic, equity, eps, sales, cash);



                        var dailyInfo = dailyInfoByBulk.Where(d => d.Ticker.Equals(company.Ticker)).SingleOrDefault();

                        var stickerPrice = CalculateStickerPrice(dailyInfo);

                        var marginOfSafety = stickerPrice / 2;

                        var scoreInfo = ScoreByBulk.Where(s => s.Ticker == company.Ticker).SingleOrDefault();

                        scoresResult.Add(new Score
                        {
                            Id = scoreInfo?.ScoreId == null ? 0 : scoreInfo.ScoreId,
                            ScoringMethodId = 1,
                            CompanyId = company.Id,
                            Score1 = (double)score,
                            StickerPrice = stickerPrice,
                            MarginOfSafety = marginOfSafety,
                            Uuid = Guid.NewGuid(),
                            UpdatedOn = DateTime.Now
                        });

                        Console.WriteLine("\tScore: " + score.ToString("n2") + "      Sticker Price: " + stickerPrice + "\n");

                    }

                    await _genericBusinessLogic.GenericTransaction(async () =>
                    {
                        _genericDao.UpdateRange<Score>(scoresResult);
                    });
                }
            //});
            return new GenericReturn { Succeeded = true, Message = "ok"};
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

            // Calculate the partial score for each indicator
            var roicPartialScore = CalculatePartialScores(roicAggregate, roicTreshold, roicWeight);
            var equityPartialScore = CalculatePartialScores(equityAggregate, equityTreshold, equityWeight);
            var epsPartialScore = CalculatePartialScores(epsAggregate, epsTreshold, epsWeight);
            var salesPartialScore = CalculatePartialScores(salesAggregate, salesTreshold, salesWeight);
            var cashPartialScore = CalculatePartialScores(cashAggregate, cashTreshold, cashWeight);

            var score = (roicPartialScore + equityPartialScore + epsPartialScore + salesPartialScore + cashPartialScore) * 100;

            return score;
        }

        private List<decimal> CalculateDelta(List<decimal> indicator)
        {
            var result = new List<decimal>();

            for (int i = 0; i < indicator.Count - 1; i++)
            {
                if (indicator[i + 1] == 0)
                {
                    result.Add(0);
                    continue;
                }

                result.Add((indicator[i] - indicator[i + 1]) / indicator[i + 1]);
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

        private decimal CalculatePartialScores(decimal aggregate, decimal treshold, decimal weight)
        {
            return aggregate / treshold * weight;
        }

        private decimal CalculateStickerPrice(DailyInfoPoco dailyInfo)
        {
            var epsTTM = dailyInfo?.EpsTTM == null ? 0 : dailyInfo.EpsTTM;

            var forwardPE = dailyInfo?.ForwardPe == null ? 0 : dailyInfo.ForwardPe;

            decimal feps = epsTTM * (decimal)(Math.Pow(((double)(1 + rate)), (double)nper));

            decimal fv = feps * forwardPE;

            return fv / (decimal)(Math.Pow((double)(1 + rate), (double)nper));
        }




    }
}
