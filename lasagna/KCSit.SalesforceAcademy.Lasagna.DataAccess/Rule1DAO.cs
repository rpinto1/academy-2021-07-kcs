using KCSit.SalesforceAcademy.Lasagna.Data;
using KCSit.SalesforceAcademy.Lasagna.Data.Pocos;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using KCSit.SalesforceAcademy.Lasagna.DataAccess.Interfaces;

namespace KCSit.SalesforceAcademy.Lasagna.DataAccess
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

        public async Task<ScorePoco> GetScoreId(string ticker, int scoringMethodId)
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

        public async Task<double> GetScore(string ticker, int scoringMethodId)
        {
            using (var context = new lasagnakcsContext())
            {
                return await ((from company in context.Companies
                                       join score in context.Scores
                                       on company.Id equals score.CompanyId
                                       where company.Ticker.Equals(ticker) &&
                                           score.ScoringMethodId == scoringMethodId
                                       select score.Score1).SingleOrDefaultAsync()) ?? 0;
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


        public async Task<CompanyScorePoco> GetInfo(AdminRule1Parameters parameters)
        {
            using (var context = new lasagnakcsContext())
            {
                var query = (from company in context.Companies
                             join country in (context.Countries.Where(c => parameters.Countries.Contains(c.Name)).AsEnumerable())
                             on company.CountryId equals country.Id
                             join dailyInfo in context.DailyInfos
                             on company.DailyInfoId equals dailyInfo.Id into LJDI
                             from dailyInfo in LJDI.DefaultIfEmpty()
                             join companyIndice in context.CompanyIndices
                             on company.Id equals companyIndice.CompanyId into LJCI
                             from companyIndex in LJCI.DefaultIfEmpty()
                             join indice in context.Indices
                             on companyIndex.IndexId equals indice.Id into LJI
                             from index in LJI.DefaultIfEmpty()
                             join score in context.Scores
                             on company.Id equals score.CompanyId into LJS
                             from score in LJS.DefaultIfEmpty()
                             join sector in context.Sectors
                             on company.SectorId equals sector.Id
                             join industry in context.Industries
                             on company.IndustryId equals industry.Id
                             where index.Name.ToLower().Contains(parameters.IndexName.ToLower()) &&
                             sector.Name.ToLower().Contains(parameters.SectorName.ToLower()) &&
                             industry.Name.ToLower().Contains(parameters.IndustryName.ToLower())
                             && score.ScoringMethodId == 1
                             group company by new CompanyPocoAuthenticated { Name = company.Name, Ticker = company.Ticker, Price = dailyInfo.StockPrice, Score = score.Score1, StickerPrice = score.StickerPrice, MarginSafety = score.MarginOfSafety } into companies
                             orderby companies.Key.Score descending
                             select new CompanyPocoAuthenticated { Name = companies.Key.Name, Ticker = companies.Key.Ticker, Price = companies.Key.Price, Score = companies.Key.Score, StickerPrice = companies.Key.StickerPrice, MarginSafety = companies.Key.MarginSafety });

                var countResults = await query.CountAsync();
                //var results = await query.Skip(parameters.Take * (parameters.Skip - 1)).Take(parameters.Take).ToListAsync();
                var results = await query.Skip(parameters.Skip).Take(parameters.Take).ToListAsync();
                var objectok = new CompanyScorePoco { CompanyPocosAuthenticated = results, Count = countResults };
                return objectok;
            }
        }


    }
}
