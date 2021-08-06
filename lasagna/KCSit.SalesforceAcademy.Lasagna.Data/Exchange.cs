using System;
using System.Collections.Generic;

#nullable disable

namespace KCSit.SalesforceAcademy.Lasagna.Data
{
    public partial class Exchange
    {
        public Exchange()
        {
            Companies = new HashSet<Company>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public Guid Uuid { get; set; }

        public virtual ICollection<Company> Companies { get; set; }
    }
}
