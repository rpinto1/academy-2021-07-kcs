using System;
using System.Collections.Generic;

#nullable disable

namespace KCSit.SalesforceAcademy.Lasagna.Data
{
    public partial class SubIndustry
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int IndustryId { get; set; }
        public Guid Uuid { get; set; }

        public virtual Industry Industry { get; set; }
    }
}
