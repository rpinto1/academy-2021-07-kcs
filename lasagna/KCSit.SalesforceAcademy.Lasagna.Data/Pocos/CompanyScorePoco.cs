using System;
using System.Collections.Generic;
using System.Text;

namespace KCSit.SalesforceAcademy.Lasagna.Data.Pocos
{
    public class CompanyScorePoco
    {
        public List<CompanyPoco> CompanyPocos { get; set; }

        public List<CompanyPocoAuthenticated> CompanyPocosAuthenticated { get; set; }
        public int Count { get; set; }

    }
}
