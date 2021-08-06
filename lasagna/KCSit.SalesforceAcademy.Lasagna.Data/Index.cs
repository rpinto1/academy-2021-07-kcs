using System;
using System.Collections.Generic;

#nullable disable

namespace KCSit.SalesforceAcademy.Lasagna.Data
{
    public partial class Index
    {
        public Index()
        {
            CompanyIndices = new HashSet<CompanyIndex>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public Guid Uuid { get; set; }

        public virtual ICollection<CompanyIndex> CompanyIndices { get; set; }
    }
}
