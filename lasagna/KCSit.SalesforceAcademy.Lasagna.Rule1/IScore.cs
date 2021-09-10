using KCSit.SalesforceAcademy.Lasagna.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KCSit.SalesforceAcademy.Lasagna.Rule1
{
    public interface IScore
    {
        Task<GenericReturn> UpdateAllScores();

        Task<GenericReturn> UpdateScore(string ticker);

    }
}
