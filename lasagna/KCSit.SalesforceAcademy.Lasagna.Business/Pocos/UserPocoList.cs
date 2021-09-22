using System;
using System.Collections.Generic;
using System.Text;

namespace KCSit.SalesforceAcademy.Lasagna.Business.Pocos
{
    public class UserPocoList
    {
        public IEnumerable<UserPoco> Users { get; set; }

        public int Total { get; set; }

    }
}
