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
                             let percentageChange = ((daily.StockPrice ?? 0) - (daily.PreviousClose ?? 0)) / ((daily.PreviousClose.Value == 0 || daily.PreviousClose == null) ? 10000000 : daily.PreviousClose) * 100
                             select new CompanyData { DisplayName = companies.Name, Symbol = companies.Ticker, RegularMarketChange = change, RegularMarketChangePercent = (Decimal)percentageChange });



                return !down ? await query.OrderByDescending(x => x.RegularMarketChangePercent).Take(10).ToListAsync() : await query.OrderBy(x => x.RegularMarketChangePercent).Take(10).ToListAsync();




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

        public async Task<CompanyScorePoco> SearchCompaniesByIndexAuthenticated(string indexName, string sectorName, string industryName, int page, List<string> countries)
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
                             join score in context.Scores
                             on company.Id equals score.CompanyId into LJS
                             from score in LJS.DefaultIfEmpty()
                             join sector in context.Sectors
                             on company.SectorId equals sector.Id
                             join industry in context.Industries
                             on company.IndustryId equals industry.Id
                             where index.Name.ToLower().Contains(indexName.ToLower()) &&
                             sector.Name.ToLower().Contains(sectorName.ToLower()) &&
                             industry.Name.ToLower().Contains(industryName.ToLower())
                             && score.ScoringMethodId == 1
                             group company by new CompanyPocoAuthenticated { Name = company.Name, Ticker = company.Ticker, Price = dailyInfo.StockPrice, Score = score.Score1, StickerPrice=score.StickerPrice, MarginSafety = score.MarginOfSafety} into companies
                             orderby companies.Key.Score descending
                             select new CompanyPocoAuthenticated { Name = companies.Key.Name, Ticker = companies.Key.Ticker, Price = companies.Key.Price, Score = companies.Key.Score, StickerPrice = companies.Key.StickerPrice, MarginSafety = companies.Key.MarginSafety });

                var countResults = await query.CountAsync();
                var results = await query.Skip(pageSize * (page - 1)).Take(pageSize).ToListAsync();
                var objectok = new CompanyScorePoco { CompanyPocosAuthenticated = results, Count = countResults };
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


    }
}
