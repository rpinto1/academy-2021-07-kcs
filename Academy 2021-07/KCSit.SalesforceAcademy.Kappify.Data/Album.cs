using System;
using System.Collections.Generic;

#nullable disable

namespace KCSit.SalesforceAcademy.Kappify.Data
{
    public partial class Album
    {
        public Album()
        {
            Songs = new HashSet<Song>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public short Year { get; set; }
        public Guid Uuid { get; set; }
        public string CoverUrl { get; set; }
        public int ArtistId { get; set; }
        public int LabelId { get; set; }

        public virtual Artist Artist { get; set; }
        public virtual Label Label { get; set; }
        public virtual ICollection<Song> Songs { get; set; }
    }
}
