using KCSit.SalesforceAcademy.Kappify.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KCSit.SalesforceAcademy.Kappify.DataAccess
{
    public class SearchDAO
    {
        public List<(Song song, string artist)> SearchSongByName(string songName)
        {
            using( var context = new academykcsContext())
            {
                //var query = context.Songs.Where(s => s.Name.ToLower().Contains(songName.ToLower()));

                var query = (from song in context.Songs
                             join artist in context.Artists
                             on song.ArtistId equals artist.Id



                             where song.Name.ToLower().Contains(songName.ToLower())
                             
                             select new { Song = song, ArtistName = artist.Name });
                

                var results = query.ToList();

                return results.Select(r => (r.Song, r.ArtistName)).ToList();
            }
        }

        public List<Song> SearchSongsByLabel(string labelString)
        {
            using (var context = new academykcsContext())
            {
                var query = (from song in context.Songs
                             join album in context.Albums
                             on song.AlbumId equals album.Id
                             join label in context.Labels
                             on album.LabelId equals label.Id
                             where label.Name.ToLower().Contains(labelString.ToLower())
                             select song
                             );
                return query.ToList(); 
            
            }
        }


        public async Task<List<Order>> HardQuery(int userId, DateTime date, string labelString)
        {
            using (var context = new academykcsContext())
            {
                return await (from order in context.Orders
                        join customer in context.Customers
                        on order.CustomerId equals customer.Id
                        join orderItem in context.OrderItems
                        on order.Id equals orderItem.OrderId
                        join song in context.Songs
                        on orderItem.SongId equals song.Id
                        join album in context.Albums
                        on song.AlbumId equals album.Id
                        join label in context.Labels
                        on album.LabelId equals label.Id
                        where label.Name.ToLower() == labelString.ToLower() &&
                              customer.Id == userId &&
                              order.DateOfOrder < date
                        select order
                    ).ToListAsync();
            }
        }
    }
}
