using System;
using System.Collections.Generic;
using System.Text;

namespace KCSit.SalesforceAcademy.Lasagna.Data.Pocos
{
    public class IncomeStatementPoco
    {
        public string Ticker { get; set; }

        public int Year { get; set; }

        public decimal Eps { get; set; }

        public decimal Sales { get; set; }

    }
}
