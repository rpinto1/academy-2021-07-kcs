using System;
using System.Collections.Generic;

#nullable disable

namespace KCSit.SalesforceAcademy.Lasagna.Data
{
    public partial class ScoringMethod
    {
        public ScoringMethod()
        {
            Scores = new HashSet<Score>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Score> Scores { get; set; }
    }
}
