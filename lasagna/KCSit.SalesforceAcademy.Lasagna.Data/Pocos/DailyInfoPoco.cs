using System;
using System.Collections.Generic;
using System.Text;

namespace KCSit.SalesforceAcademy.Lasagna.Data.Pocos
{
    public class DailyInfoPoco
    {
        public string Ticker { get; set; }
        public decimal ForwardPe { get; set; }
        public decimal EpsTTM { get; set; }
    }
}
