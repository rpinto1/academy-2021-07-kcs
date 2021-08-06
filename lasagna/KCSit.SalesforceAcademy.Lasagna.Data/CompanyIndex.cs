using System;
using System.Collections.Generic;

#nullable disable

namespace KCSit.SalesforceAcademy.Lasagna.Data
{
    public partial class CompanyIndex
    {
        public int Id { get; set; }
        public int? CompanyId { get; set; }
        public int? IndexId { get; set; }
        public Guid Uuid { get; set; }

        public virtual Company Company { get; set; }
        public virtual Index Index { get; set; }
    }
}
