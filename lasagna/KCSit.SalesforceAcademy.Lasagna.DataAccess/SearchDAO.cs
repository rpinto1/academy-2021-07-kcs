using KCSit.SalesforceAcademy.Lasagna.DataAccess.Interfaces;
using KCSit.SalesforceAcademy.Lasagna.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        public SubIndustry GetSub(string name)
        {
            using (var context = new lasagnakcsContext())
            {
                return context.Set<SubIndustry>().Where(item => item.Name == name).SingleOrDefault();

            }
        }

        //todos os métodos devem ter um verbo
        //o método deve ser assíncrono
        //para ter paginação, vai ter que ter os parâmetros skip e take
        public List<Company> SearchCompaniesBySearchBar(string search)
        {
            using (var context = new lasagnakcsContext())
            {

                var query = (from test in context.Companies
                             //considerar usar o contains em vez do startswith
                             where test.Name.ToLower().Contains(search.ToLower())
                             || test.Ticker.ToLower().Contains(search.ToLower())
                             //deves devolver um POCO em vez de Company
                             select new Company {Name=test.Name,Ticker=test.Ticker});

                //o pedido deve ser assíncrono
                var results = query
                    //.Skip(20).Take(10)
                    .ToList();

                return results;
            }
        }
        public List<Industry> SearchIndustiesBySector(string sectorName)
        {
            using (var context = new lasagnakcsContext())
            {

                var query = (from sector in context.Sectors
                             join industry in context.Industries
                             on sector.Id equals industry.SectorId
                             where sector.Name.ToLower().Contains(sectorName.ToLower())
                             select industry);


                var results = query.ToList();

                return results;
            }
        }
        public async Task<List<Company>> SearchCompaniesByIndex(string? indexName, string? sectorName, string? industryName)
        {
            using (var context = new lasagnakcsContext())
            {
                if (indexName == null)
                {
                    indexName = "";
                }
                if (sectorName == null)
                {
                    sectorName = "";
                }
                if (industryName == null)
                {
                    industryName = "";
                }


                var query = (from company in context.Companies
                             join companyIndice in context.CompanyIndices
                             on company.Id equals companyIndice.CompanyId
                             join indice in context.Indices
                             on companyIndice.IndexId equals indice.Id
                             join sector in context.Sectors
                             on company.SectorId equals sector.Id
                             join industry in context.Industries
                             on company.IndustryId equals industry.Id
                             where indice.Name.ToLower().Contains(indexName.ToLower()) &&
                             sector.Name.ToLower().Contains(sectorName.ToLower()) &&
                             industry.Name.ToLower().Contains(industryName.ToLower())
                             select company);


                var results = query.ToListAsync();

                return await results;
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
