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
                             on company.DailyInfoId equals dailyInfo.Id into LJDI from dailyInfo in LJDI.DefaultIfEmpty()
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
                             select new CompanyPoco { Name = companies.Key.Name,Ticker= companies.Key.Ticker, Price= companies.Key.Price });

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
                return (from company in context.Companies
                        select company.Id).Count();
            }
        }


        public async Task<IEnumerable> GetCompanies()
        {
            using (var context = new lasagnakcsContext())
            {
                return (from company in context.Companies
                        select new CompanyPoco
                        {
                            Id = company.Id,
                            Ticker = company.Ticker,
                            Name = company.Name
                        })

                        .ToList();
            }
        }

        public async Task<IEnumerable<CompanyPoco>> GetCompaniesByBulk(int skip, int take)
        {
            using (var context = new lasagnakcsContext())
            {
                return (from company in context.Companies
                        select new CompanyPoco
                        {
                            Id = company.Id,
                            Ticker = company.Ticker,
                            Name = company.Name
                        })

                        .Skip(skip)
                        .Take(take)
                        .ToList();
            }
        }
        public IEnumerable<KeyRatiosPoco> GetKeyRatios(string ticker)
        {
            using (var context = new lasagnakcsContext())
            {
                return (from company in context.Companies
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
                        })

                        .Take(20)
                        .ToList();
            }
        }


        public IEnumerable<KeyRatiosPoco> GetKeyRatiosByBulk(List<string> tickers)
        {
            using (var context = new lasagnakcsContext())
            {
                return (from company in context.Companies.Where(c => tickers.Contains(c.Ticker))
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
                        })

                        .Take(20 * tickers.Count)
                        .ToList();
            }
        }

        public IEnumerable<BalanceSheetPoco> GetBalanceSheet(string ticker)
        {
            using (var context = new lasagnakcsContext())
            {
                return (from company in context.Companies
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
                        .ToList();
            }
        }


        public IEnumerable<BalanceSheetPoco> GetBalanceSheetByBulk(List<string> tickers)
        {
            using (var context = new lasagnakcsContext())
            {
                return (from company in context.Companies.Where(c => tickers.Contains(c.Ticker))
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
                        .ToList();
            }
        }


        public IEnumerable<IncomeStatementPoco> GetIncomeStatement(string ticker)
        {
            using (var context = new lasagnakcsContext())
            {
                return (from company in context.Companies
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
                        .ToList();
            }
        }


        public IEnumerable<IncomeStatementPoco> GetIncomeStatementByBulk(List<string> tickers)
        {
            using (var context = new lasagnakcsContext())
            {
                return (from company in context.Companies.Where(c => tickers.Contains(c.Ticker))
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
                        .ToList();
            }
        }



        public DailyInfoPoco GetDailyInfo(string ticker)
        {
            using (var context = new lasagnakcsContext())
            {
                return (from company in context.Companies
                        join dailyInfo in context.DailyInfos
                        //on company.Id equals dailyInfo.Id
                        on company.DailyInfoId equals dailyInfo.Id
                        where company.Ticker.Equals(ticker)

                        select new DailyInfoPoco
                        {
                            Ticker = company.Ticker,
                            ForwardPe = (decimal?)dailyInfo.ForwardPe ?? 0,
                            EpsTTM = (decimal?)dailyInfo.EpsTTM ?? 0
                        }).FirstOrDefault();
            }
        }

        public IEnumerable<DailyInfoPoco> GetDailyInfoByBulk(List<string> tickers)
        {
            using (var context = new lasagnakcsContext())
            {
                return (from company in context.Companies
                        join dailyInfo in context.DailyInfos
                        //on company.DailyInfoId equals dailyInfo.Id
                        on company.Id equals dailyInfo.Id
                        where tickers.Contains(company.Ticker)

                        select new DailyInfoPoco
                        {
                            Ticker = company.Ticker,
                            ForwardPe = (decimal?)dailyInfo.ForwardPe ?? 0,
                            EpsTTM = (decimal?)dailyInfo.EpsTTM ?? 0
                        }).ToList();
            }
        }

        public ScorePoco GetScore(string ticker, int scoringMethodId)
        {
            using (var context = new lasagnakcsContext())
            {
                return (from company in context.Companies
                        join score in context.Scores
                        on company.Id equals score.CompanyId
                        where company.Ticker.Equals(ticker) &&
                            score.ScoringMethodId == scoringMethodId
                        select new ScorePoco
                        {
                            Ticker = company.Ticker,
                            ScoreId = score.Id
                        }
                        ).SingleOrDefault();
            }
        }

        public IEnumerable<ScorePoco> GetScoreByBulk(List<string> tickers, int scoringMethodId)
        {
            using (var context = new lasagnakcsContext())
            {
                return (from company in context.Companies
                        join score in context.Scores
                        on company.Id equals score.CompanyId
                        where tickers.Contains(company.Ticker) &&
                            score.ScoringMethodId == scoringMethodId
                        select new ScorePoco
                        {
                            Ticker = company.Ticker,
                            ScoreId = score.Id
                        }
                        ).ToList();
            }
        }
    
    
    }
}
