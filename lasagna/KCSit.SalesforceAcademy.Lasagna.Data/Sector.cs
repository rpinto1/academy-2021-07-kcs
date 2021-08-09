using System;
using System.Collections.Generic;

#nullable disable

namespace KCSit.SalesforceAcademy.Lasagna.Data
{
    public partial class Sector
    {
        public Sector()
        {
            Companies = new HashSet<Company>();
            Industries = new HashSet<Industry>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public Guid Uuid { get; set; }

        public virtual ICollection<Company> Companies { get; set; }
        public virtual ICollection<Industry> Industries { get; set; }
    }
}
