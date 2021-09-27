using KCSit.SalesforceAcademy.Lasagna.Data;
using KCSit.SalesforceAcademy.Lasagna.Data.Pocos;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using KCSit.SalesforceAcademy.Lasagna.DataAccess.Interfaces;
using System;
using System.Web;
using System.Text.Json;

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

        public async Task<IEnumerable<string>> GetTickersByBulk(int skip, int take)
        {
            using (var context = new lasagnakcsContext())
            {
                return await (from company in context.Companies
                              select company.Ticker)

                        .Skip(skip)
                        .Take(take)
                        .ToListAsync();
            }
        }


        public async Task<IEnumerable<CompanyPoco>> GetCompaniesByTickerList(List<string> tickers)
        {
            using (var context = new lasagnakcsContext())
            {
                return await (from company in context.Companies.Where(c => tickers.Contains(c.Ticker))

                              select new CompanyPoco
                              {
                                  Id = company.Id,
                                  Ticker = company.Ticker,
                                  Name = company.Name
                              })

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


        public async Task<AdminRule1PocoList> GetInfo(string queryString)
        {
            using (var context = new lasagnakcsContext())
            {

                if (String.IsNullOrEmpty(queryString))
                {
                    queryString = "?filter=%7B%7D&range=%5B0%2C9%5D&sort=%5B%22score%22%2C%22ASC%22%5D";
                }

                var filterStr = HttpUtility.ParseQueryString(queryString).Get("filter");

                var filter = new AdminRule1Parameters();
                if (filterStr != null)
                {
                    filter = JsonSerializer.Deserialize<AdminRule1Parameters>(filterStr);
                    filter.id ??= "";
                    filter.name ??= "";
                    filter.country ??= "";
                    filter.exchange ??= "";
                    filter.sector ??= "";
                    filter.industry ??= "";
                    filter.currency ??= "";
                }

                // [0, 9],  [10, 19],  [20, 25] ...
                var skip = 0;
                var take = 0;
                var rangeStr = HttpUtility.ParseQueryString(queryString).Get("range");
                if (rangeStr != null)
                {
                    var first = int.Parse(rangeStr.Substring(1, rangeStr.IndexOf(",") - rangeStr.IndexOf("[") - 1));
                    var last = int.Parse(rangeStr.Substring(rangeStr.IndexOf(",") + 1, rangeStr.IndexOf("]") - rangeStr.IndexOf(",") - 1));
                    skip = first;
                    take = last - first + 1;
                }

                // sort=["id","ASC"]"
                var orderByField = "";
                var orderByDirection = "Ascending";
                var sortStr = HttpUtility.ParseQueryString(queryString).Get("sort");
                if (sortStr != null)
                {
                    orderByField = sortStr.Split("\"")[1];
                    orderByField = orderByField.ToUpper().ElementAt(0) + orderByField.Substring(1);
                    orderByDirection = sortStr.Contains("ASC") ? "Ascending" : "Descending";
                }


                var query = (from company in context.Companies
                             join country in context.Countries
                             on company.CountryId equals country.Id
                             join exchage in context.Exchanges
                             on company.ExchangeId equals exchage.Id
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

                             where
                                company.Ticker.ToLower().Contains(filter.id.ToLower()) &&
                                company.Name.ToLower().Contains(filter.name.ToLower()) &&
                                country.FullName.ToLower().Contains(filter.country.ToLower()) &&
                                exchage.Name.ToLower().Contains(filter.exchange.ToLower()) &&
                                sector.Name.ToLower().Contains(filter.sector.ToLower()) &&
                                industry.Name.ToLower().Contains(filter.industry.ToLower()) &&
                                company.Currency.ToLower().Contains(filter.currency) &&
                                score.ScoringMethodId == 1

                             group company by new AdminRule1Poco
                             {
                                 Id = company.Ticker,
                                 Name = company.Name,
                                 Country = country.FullName,
                                 Exchange = exchage.Name,
                                 Sector = sector.Name,
                                 Industry = industry.Name,
                                 Price = dailyInfo.StockPrice,
                                 Currency = company.Currency,
                                 Score = score.Score1,
                                 StickerPrice = score.StickerPrice,
                                 MarginSafety = score.MarginOfSafety,
                                 UpdatedOn = score.UpdatedOn,
                             } into companies

                             select new AdminRule1Poco
                             {
                                 Id = companies.Key.Id,
                                 Name = companies.Key.Name,
                                 Country = companies.Key.Country,
                                 Exchange = companies.Key.Exchange,
                                 Sector = companies.Key.Sector,
                                 Industry = companies.Key.Industry,
                                 Price = companies.Key.Price,
                                 Currency = companies.Key.Currency,
                                 Score = companies.Key.Score,
                                 StickerPrice = companies.Key.StickerPrice,
                                 MarginSafety = companies.Key.MarginSafety,
                                 UpdatedOn = companies.Key.UpdatedOn,
                             });

                var results = await query
                    .OrderBy(orderByField ?? "Score", orderByDirection ?? "DESC")
                    .Skip(skip)
                    .Take(take)
                    .ToListAsync();

                var countResults = await query.CountAsync();

                var objectok = new AdminRule1PocoList { Result = results, Total = countResults };

                return objectok;
            }
        }


    }
}
