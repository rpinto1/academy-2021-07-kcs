using System;
using System.Collections.Generic;

#nullable disable

namespace KCSit.SalesforceAcademy.Lasagna.Data
{
    public partial class Industry
    {
        public Industry()
        {
            Companies = new HashSet<Company>();
            SubIndustries = new HashSet<SubIndustry>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public Guid Uuid { get; set; }

        public virtual ICollection<Company> Companies { get; set; }
        public virtual ICollection<SubIndustry> SubIndustries { get; set; }
    }
}
