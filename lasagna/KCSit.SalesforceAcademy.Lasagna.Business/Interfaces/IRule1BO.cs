using KCSit.SalesforceAcademy.Lasagna.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KCSit.SalesforceAcademy.Lasagna.Business.Interfaces
{
    public interface IRule1BO
    {
        Task<GenericReturn> UpdateAllScores();

        Task<GenericReturn> UpdateScore(string ticker);

    }
}
