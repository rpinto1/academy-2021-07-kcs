using KCSit.SalesforceAcademy.Lasagna.DataAccess.Interfaces;
using KCSit.SalesforceAcademy.Lasagna.Data;
using Microsoft.EntityFrameworkCore;
using KCSit.SalesforceAcademy.Lasagna.Data.Pocos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Collections;


namespace KCSit.SalesforceAcademy.Lasagna.DataAccess
{
    public class SearchDAO : ISearchDAO
    {

        public int Get(string ticker)
        {
            using (var context = new lasagnakcsContext())
            {
                return context.Set<Company>().Where(item => item.Ticker == ticker).SingleOrDefault().Id;
            }
        }

        public async Task<int> GetAsync(string ticker)
        {
            using (var context = new lasagnakcsContext())
            {
                var company = await context.Set<Company>().Where(item => item.Ticker == ticker).SingleOrDefaultAsync();

                return company.Id;
            }
        }

        public Industry GetIndustry(string name)
        {
            using (var context = new lasagnakcsContext())
            {
                return context.Set<Industry>().Where(item => item.Name == name).SingleOrDefault();

            }
        }

        //public SubIndustry GetSub(string name)
        //{
        //    using (var context = new lasagnakcsContext())
        //    {
        //        return context.Set<SubIndustry>().Where(item => item.Name == name).SingleOrDefault();

        //    }
        //  }

        public async Task<List<CompanyPoco>> SearchCompaniesBySearchQuery(string search, int pageSize, int pageNumber)
        {

            using (var context = new lasagnakcsContext())
            {

                var query = (from companies in context.Companies
                             where companies.Name.ToLower().Contains(search.ToLower())
                             || companies.Ticker.ToLower().Contains(search.ToLower())
                             select new CompanyPoco { Name = companies.Name, Ticker = companies.Ticker })
                             .Skip(pageSize * pageNumber).Take(pageSize);

                var results = await query.ToListAsync();

                return results;
            }
        }
        public async Task<List<CompanyData>> SearchCompaniesPrice(bool down, List<string> countries)
        {

            using (var context = new lasagnakcsContext())
            {

                var query = (from companies in context.Companies
                             join daily in context.DailyInfos
                             on companies.DailyInfoId equals daily.Id
                             join country in (context.Countries.Where(c => countries.Contains(c.Name)).AsEnumerable())
                            on companies.CountryId equals country.Id
                            where daily.UpdatedOn.ToString() != "0001-01-01 00:00:00.0000000"
                             let change = (daily.StockPrice ?? 0) - (daily.PreviousClose ?? 0)
                             let percentageChange = ((daily.StockPrice ?? 0) - (daily.PreviousClose ?? 0)) /(( daily.PreviousClose.Value == 0 || daily.PreviousClose == null) ? 10000000 : daily.PreviousClose )  *100
                             select new CompanyData { DisplayName = companies.Name, Symbol = companies.Ticker, RegularMarketChange = change , RegularMarketChangePercent = (Decimal)  percentageChange }) ;


                
                    return !down? await query.OrderByDescending(x=>x.RegularMarketChangePercent).Take(10).ToListAsync() : await query.OrderBy(x => x.RegularMarketChangePercent).Take(10).ToListAsync();




            }
        }
        public async Task<List<Industry>> SearchIndustiesBySector(string sectorName)
        {
            using (var context = new lasagnakcsContext())
            {

                var query = (from sector in context.Sectors
                             join industry in context.Industries
                             on sector.Id equals industry.SectorId
                             where sector.Name.ToLower().Contains(sectorName.ToLower())
                             select industry);


                var results = await query.ToListAsync();

                return results;
            }
        }
        public async Task<CompanyScorePoco> SearchCompaniesByIndex(string indexName, string sectorName, string industryName, int page, List<string> countries)
        {
            var pageSize = 10;
            using (var context = new lasagnakcsContext())
            {

                var query = (from company in context.Companies
                             join country in (context.Countries.Where(c => countries.Contains(c.Name)).AsEnumerable())
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
                             join sector in context.Sectors
                             on company.SectorId equals sector.Id
                             join industry in context.Industries
                             on company.IndustryId equals industry.Id
                             where index.Name.ToLower().Contains(indexName.ToLower()) &&
                             sector.Name.ToLower().Contains(sectorName.ToLower()) &&
                             industry.Name.ToLower().Contains(industryName.ToLower())
                             group company by new CompanyPoco { Name = company.Name, Ticker = company.Ticker, Price = dailyInfo.StockPrice } into companies
                             select new CompanyPoco { Name = companies.Key.Name, Ticker = companies.Key.Ticker, Price = companies.Key.Price });

                var countResults = await query.CountAsync();
                var results = await query.Skip(pageSize * (page - 1)).Take(pageSize).ToListAsync();
                var objectok = new CompanyScorePoco { CompanyPocos = results, Count = countResults };
                return objectok;
            }
        }

        //public List<(Song song, string artist)> SearchSongByName(string songName)
        //{
        //    using( var context = new academykcsContext())
        //    {
        //        //var query = context.Songs.Where(s => s.Name.ToLower().Contains(songName.ToLower()));

        //        var query = (from song in context.Songs
        //                     join artist in context.Artists
        //                     on song.ArtistId equals artist.Id



        //                     where song.Name.ToLower().Contains(songName.ToLower())

        //                     select new { Song = song, ArtistName = artist.Name });


        //        var results = query.ToList();

        //        return results.Select(r => (r.Song, r.ArtistName)).ToList();
        //    }
        //}

        //public List<Song> SearchSongsByLabel(string labelString)
        //{
        //    using (var context = new academykcsContext())
        //    {
        //        var query = (from song in context.Songs
        //                     join album in context.Albums
        //                     on song.AlbumId equals album.Id
        //                     join label in context.Labels
        //                     on album.LabelId equals label.Id
        //                     where label.Name.ToLower().Contains(labelString.ToLower())
        //                     select song
        //                     );
        //        return query.ToList(); 

        //    }
        //}


        //public async Task<List<Order>> HardQuery(int userId, DateTime date, string labelString)
        //{
        //    using (var context = new academykcsContext())
        //    {
        //        return await (from order in context.Orders
        //                join customer in context.Customers
        //                on order.CustomerId equals customer.Id
        //                join orderItem in context.OrderItems
        //                on order.Id equals orderItem.OrderId
        //                join song in context.Songs
        //                on orderItem.SongId equals song.Id
        //                join album in context.Albums
        //                on song.AlbumId equals album.Id
        //                join label in context.Labels
        //                on album.LabelId equals label.Id
        //                where label.Name.ToLower() == labelString.ToLower() &&
        //                      customer.Id == userId &&
        //                      order.DateOfOrder < date
        //                select order
        //            ).ToListAsync();
        //    }
        //}




        public async Task<int> GetCompaniesCount()
        {
            using (var context = new lasagnakcsContext())
            {
                return await (from company in context.Companies
                              select company.Id).CountAsync();
            }
        }


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

        public async Task<IEnumerable<CompanyPoco>> GetCompaniesByBulk(int Id)
        {
            using (var context = new lasagnakcsContext())
            {
                return await (from company in context.Companies 
                              join PortfolioCompany in context.PortfolioCompanies
                              on company.Id equals PortfolioCompany.CompanyId
                              where PortfolioCompany.PortfolioId==Id
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
                              into reports

                              from report in reports
                              join keyRatios in context.KeyRatios
                              on report.KeyRatioId equals keyRatios.Id

                              where company.Ticker.Equals(ticker)
                              orderby report.Year descending

                             select new KeyRatiosPoco
                              {
                                  Ticker = company.Ticker,
                                  Year = (int?)report.Year ?? 0,
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
                              into reports

                              from report in reports
                              join keyRatios in context.KeyRatios
                              on report.KeyRatioId equals keyRatios.Id

                              where tickers.Contains(company.Ticker)
                              orderby company.Ticker, report.Year descending

                              select new KeyRatiosPoco
                              {
                                  Ticker = company.Ticker,
                                  Year = (int?)report.Year ?? 0,
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
                              into reports

                              from report in reports
                              join balanceSheet in context.BalanceSheets
                              on report.BalanceSheetId equals balanceSheet.Id

                              where company.Ticker.Equals(ticker)
                              orderby report.Year descending

                              select new BalanceSheetPoco
                              {
                                  Ticker = company.Ticker,
                                  Year = (int?)report.Year ?? 0,
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
                              into reports

                              from report in reports
                              join balanceSheet in context.BalanceSheets
                              on report.BalanceSheetId equals balanceSheet.Id

                              where tickers.Contains(company.Ticker)
                              orderby company.Ticker, report.Year descending

                              select new BalanceSheetPoco
                              {
                                  Ticker = company.Ticker,
                                  Year = (int?)report.Year ?? 0,
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
                              into reports

                              from report in reports
                              join incomeStatement in context.IncomeStatements
                              on report.IncomeStatementId equals incomeStatement.Id

                              where company.Ticker.Equals(ticker)
                              orderby report.Year descending

                              select new IncomeStatementPoco
                              {
                                  Ticker = company.Ticker,
                                  Year = (int?)report.Year ?? 0,
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
                              into reports

                              from report in reports
                              join incomeStatement in context.IncomeStatements
                              on report.IncomeStatementId equals incomeStatement.Id

                              where tickers.Contains(company.Ticker)
                              orderby company.Ticker, report.Year descending

                              select new IncomeStatementPoco
                              {
                                  Ticker = company.Ticker,
                                  Year = (int?)report.Year ?? 0,
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
                              //on company.Id equals dailyInfo.Id
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
                              //on company.DailyInfoId equals dailyInfo.Id
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


        public async Task<List<PortfolioPoco>> GetPortfolios(Guid userId)
        {
            using (var context = new lasagnakcsContext())
            {

                return await (from portfolio in context.Portfolios
                              where portfolio.UserId == userId.ToString()

                              select new PortfolioPoco
                              {
                                  PortfolioId = portfolio.Uuid,
                                  PortfolioName = portfolio.Name,
                                  
                              })

                        .ToListAsync();
            }
        }

        public async Task<List<PortfolioCompanyPoco>> GetCompaniesByPortfolio(Guid portfolioUuid)
        {
            using (var context = new lasagnakcsContext())
            {
                return await (from portfolio in context.Portfolios
                              join portfolioCompany in context.PortfolioCompanies
                              on portfolio.Id equals portfolioCompany.PortfolioId 
                              join company in context.Companies
                              on portfolioCompany.CompanyId equals company.Id
                              
                              where portfolio.Uuid == portfolioUuid

                              select new PortfolioCompanyPoco
                              {
                                  Ticker = company.Ticker,
                                  Name = company.Name,
                                  
                              })

                        //.Take(20)
                        .ToListAsync();
            }
        }

        public async Task<List<PortfolioCompanyValuesPoco>> GetCompanyValuesByTicker(string ticker)
        {
            using (var context = new lasagnakcsContext())
            {

                var keyRatiosList = (await GetKeyRatios(ticker)).ToList();
                var balanceSheetList = (await GetBalanceSheet(ticker)).ToList();
                var incomeStatementList = (await GetIncomeStatement(ticker)).ToList();

                var roic = (from kr in keyRatiosList
                            select kr.Roic).ToList();

                var equity = (from bs in balanceSheetList
                              select bs.Equity).ToList();

                var eps = (from incStat in incomeStatementList
                           select incStat.Eps).ToList();

                var sales = (from incStat in incomeStatementList
                             select incStat.Sales).ToList();

                var cash = (from bs in balanceSheetList
                            select bs.Cash).ToList();

                var list = new List<PortfolioCompanyValuesPoco>();


                for (int i = 0; i < keyRatiosList.Count; i++)
                {
                    list.Add(new PortfolioCompanyValuesPoco
                    {
                        Year = keyRatiosList[i].Year,
                        ROIC = roic[i],
                        Equity = equity[i],
                        EPS = eps[i],
                        Sales = sales[i],
                        Cash = cash[i]
                    });

                }

                return await Task.FromResult(list);
            }
        }

        public async Task<int> GetPortfolioId(Guid portfolioUuid)
        {
            using (var context = new lasagnakcsContext())
            {

                var activePortfolio = await (
                    from portfolio in context.Portfolios
                    where portfolio.Uuid == portfolioUuid

                    select new Portfolio())
                    .ToListAsync();

                return activePortfolio.GetEnumerator().Current.Id;

            }
        }

    }
}
