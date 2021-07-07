using System;
using System.Collections.Generic;

#nullable disable

namespace KCSit.SalesforceAcademy.Kappify.Data
{
    public partial class Artist
    {
        public Artist()
        {
            Albums = new HashSet<Album>();
            Songs = new HashSet<Song>();
        }

        public string Name { get; set; }
        public string Bio { get; set; }
        public string Genre { get; set; }
        public int Id { get; set; }
        public Guid Uuid { get; set; }
        public string ImageUrl { get; set; }

        public virtual ICollection<Album> Albums { get; set; }
        public virtual ICollection<Song> Songs { get; set; }
    }
}
