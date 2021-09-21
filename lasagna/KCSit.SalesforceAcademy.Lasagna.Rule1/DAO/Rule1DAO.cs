using KCSit.SalesforceAcademy.Lasagna.Data;
using KCSit.SalesforceAcademy.Lasagna.Data.Pocos;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace KCSit.SalesforceAcademy.Lasagna.Rule1
{
    public class Rule1DAO : IRule1DAO
    {

        public async Task<IEnumerable> GetCompanies()
        {
            using (var context = new lasagnakcsContext())
            {
                return await (from company in context.Companies
                              select new CompanyPoco
                              {
                                  Id = company.Id,
                                  Ticker = company.Ticker,
                                  Name = company.Name
                              })

                        .ToListAsync();
            }
        }

        public async Task<IEnumerable<CompanyPoco>> GetCompaniesByBulk(int skip, int take)
        {
            using (var context = new lasagnakcsContext())
            {
                return await (from company in context.Companies
                              select new CompanyPoco
                              {
                                  Id = company.Id,
                                  Ticker = company.Ticker,
                                  Name = company.Name
                              })

                        .Skip(skip)
                        .Take(take)
                        .ToListAsync();
            }
        }


        public async Task<IEnumerable<KeyRatiosPoco>> GetKeyRatios(string ticker)
        {
            using (var context = new lasagnakcsContext())
            {
                return await (from company in context.Companies
                              join yearlyReport in context.YearlyReports
                              on company.Id equals yearlyReport.CompanyId
                              join keyRatios in context.KeyRatios
                              on yearlyReport.KeyRatioId equals keyRatios.Id

                              where company.Ticker.Equals(ticker)
                              orderby yearlyReport.Year descending

                              select new KeyRatiosPoco
                              {
                                  Ticker = company.Ticker,
                                  Year = (int?)yearlyReport.Year ?? 0,
                                  Roic = (decimal?)keyRatios.ReturnOnInvestedCapital ?? 0,
                                  PriceToEarnings = (decimal?)keyRatios.PriceToEarnings ?? 0,
                              })

                        .Take(20)
                        .ToListAsync();
            }
        }


        public async Task<IEnumerable<KeyRatiosPoco>> GetKeyRatiosByBulk(List<string> tickers)
        {
            using (var context = new lasagnakcsContext())
            {
                return await (from company in context.Companies.Where(c => tickers.Contains(c.Ticker))
                              join yearlyReport in context.YearlyReports
                              on company.Id equals yearlyReport.CompanyId
                              join keyRatios in context.KeyRatios
                              on yearlyReport.KeyRatioId equals keyRatios.Id

                              where tickers.Contains(company.Ticker)
                              orderby company.Ticker, yearlyReport.Year descending

                              select new KeyRatiosPoco
                              {
                                  Ticker = company.Ticker,
                                  Year = (int?)yearlyReport.Year ?? 0,
                                  Roic = (decimal?)keyRatios.ReturnOnInvestedCapital ?? 0,
                                  PriceToEarnings = (decimal?)keyRatios.PriceToEarnings ?? 0,
                              })

                        .Take(20 * tickers.Count)
                        .ToListAsync();
            }
        }

        public async Task<IEnumerable<BalanceSheetPoco>> GetBalanceSheet(string ticker)
        {
            using (var context = new lasagnakcsContext())
            {
                return await (from company in context.Companies
                              join yearlyReport in context.YearlyReports
                              on company.Id equals yearlyReport.CompanyId
                              join balanceSheet in context.BalanceSheets
                              on yearlyReport.BalanceSheetId equals balanceSheet.Id

                              where company.Ticker.Equals(ticker)
                              orderby yearlyReport.Year descending

                              select new BalanceSheetPoco
                              {
                                  Ticker = company.Ticker,
                                  Year = (int?)yearlyReport.Year ?? 0,
                                  Equity = (decimal?)balanceSheet.ShareholdersEquity ?? 0,
                                  Cash = (decimal?)balanceSheet.CashAndEquivalents ?? 0
                              })

                        .Take(20)
                        .ToListAsync();
            }
        }


        public async Task<IEnumerable<BalanceSheetPoco>> GetBalanceSheetByBulk(List<string> tickers)
        {
            using (var context = new lasagnakcsContext())
            {
                return await (from company in context.Companies.Where(c => tickers.Contains(c.Ticker))
                              join yearlyReport in context.YearlyReports
                              on company.Id equals yearlyReport.CompanyId
                              join balanceSheet in context.BalanceSheets
                              on yearlyReport.BalanceSheetId equals balanceSheet.Id

                              where tickers.Contains(company.Ticker)
                              orderby company.Ticker, yearlyReport.Year descending

                              select new BalanceSheetPoco
                              {
                                  Ticker = company.Ticker,
                                  Year = (int?)yearlyReport.Year ?? 0,
                                  Equity = (decimal?)balanceSheet.ShareholdersEquity ?? 0,
                                  Cash = (decimal?)balanceSheet.CashAndEquivalents ?? 0
                              })

                        .Take(20 * tickers.Count)
                        .ToListAsync();
            }
        }


        public async Task<IEnumerable<IncomeStatementPoco>> GetIncomeStatement(string ticker)
        {
            using (var context = new lasagnakcsContext())
            {
                return await (from company in context.Companies
                              join yearlyReport in context.YearlyReports
                              on company.Id equals yearlyReport.CompanyId
                              join incomeStatement in context.IncomeStatements
                              on yearlyReport.IncomeStatementId equals incomeStatement.Id

                              where company.Ticker.Equals(ticker)
                              orderby yearlyReport.Year descending

                              select new IncomeStatementPoco
                              {
                                  Ticker = company.Ticker,
                                  Year = (int?)yearlyReport.Year ?? 0,
                                  Eps = (decimal?)incomeStatement.Epsbasic ?? 0,
                                  Sales = (decimal?)incomeStatement.Revenue ?? 0
                              })

                        .Take(20)
                        .ToListAsync();
            }
        }


        public async Task<IEnumerable<IncomeStatementPoco>> GetIncomeStatementByBulk(List<string> tickers)
        {
            using (var context = new lasagnakcsContext())
            {
                return await (from company in context.Companies.Where(c => tickers.Contains(c.Ticker))
                              join yearlyReport in context.YearlyReports
                              on company.Id equals yearlyReport.CompanyId
                              join incomeStatement in context.IncomeStatements
                              on yearlyReport.IncomeStatementId equals incomeStatement.Id

                              where tickers.Contains(company.Ticker)
                              orderby company.Ticker, yearlyReport.Year descending

                              select new IncomeStatementPoco
                              {
                                  Ticker = company.Ticker,
                                  Year = (int?)yearlyReport.Year ?? 0,
                                  Eps = (decimal?)incomeStatement.Epsbasic ?? 0,
                                  Sales = (decimal?)incomeStatement.Revenue ?? 0
                              })

                        .Take(20 * tickers.Count)
                        .ToListAsync();
            }
        }



        public async Task<DailyInfoPoco> GetDailyInfo(string ticker)
        {
            using (var context = new lasagnakcsContext())
            {
                return await (from company in context.Companies
                              join dailyInfo in context.DailyInfos
                              on company.DailyInfoId equals dailyInfo.Id
                              where company.Ticker.Equals(ticker)

                              select new DailyInfoPoco
                              {
                                  Ticker = company.Ticker,
                                  ForwardPe = (decimal?)dailyInfo.ForwardPe ?? 0,
                                  EpsTTM = (decimal?)dailyInfo.EpsTTM ?? 0
                              }).FirstOrDefaultAsync();
            }
        }

        public async Task<IEnumerable<DailyInfoPoco>> GetDailyInfoByBulk(List<string> tickers)
        {
            using (var context = new lasagnakcsContext())
            {
                return await (from company in context.Companies
                              join dailyInfo in context.DailyInfos
                              on company.Id equals dailyInfo.Id
                              where tickers.Contains(company.Ticker)

                              select new DailyInfoPoco
                              {
                                  Ticker = company.Ticker,
                                  ForwardPe = (decimal?)dailyInfo.ForwardPe ?? 0,
                                  EpsTTM = (decimal?)dailyInfo.EpsTTM ?? 0
                              }).ToListAsync();
            }
        }

        public async Task<ScorePoco> GetScore(string ticker, int scoringMethodId)
        {
            using (var context = new lasagnakcsContext())
            {
                return await (from company in context.Companies
                              join score in context.Scores
                              on company.Id equals score.CompanyId
                              where company.Ticker.Equals(ticker) &&
                                  score.ScoringMethodId == scoringMethodId
                              select new ScorePoco
                              {
                                  Ticker = company.Ticker,
                                  ScoreId = score.Id
                              }
                        ).SingleOrDefaultAsync();
            }
        }

        public async Task<IEnumerable<ScorePoco>> GetScoreByBulk(List<string> tickers, int scoringMethodId)
        {
            using (var context = new lasagnakcsContext())
            {
                return await (from company in context.Companies
                              join score in context.Scores
                              on company.Id equals score.CompanyId
                              where tickers.Contains(company.Ticker) &&
                                  score.ScoringMethodId == scoringMethodId
                              select new ScorePoco
                              {
                                  Ticker = company.Ticker,
                                  ScoreId = score.Id
                              }
                        ).ToListAsync();
            }
        }


    }
}
