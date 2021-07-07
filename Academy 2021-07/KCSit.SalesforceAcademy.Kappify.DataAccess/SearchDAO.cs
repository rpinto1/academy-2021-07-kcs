using KCSit.SalesforceAcademy.Kappify.Data;
using KCSit.SalesforceAcademy.Kappify.Data.Pocos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
    }
}
