using System;
using System.Collections.Generic;

#nullable disable

namespace KCSit.SalesforceAcademy.Kappify.Data
{
    public partial class Label
    {
        public Label()
        {
            Albums = new HashSet<Album>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string Website { get; set; }
        public Guid Uuid { get; set; }

        public virtual ICollection<Album> Albums { get; set; }
    }
}
