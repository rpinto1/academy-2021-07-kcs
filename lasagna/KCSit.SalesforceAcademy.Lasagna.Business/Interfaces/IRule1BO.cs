using KCSit.SalesforceAcademy.Lasagna.Business;
using KCSit.SalesforceAcademy.Lasagna.Data.Pocos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KCSit.SalesforceAcademy.Lasagna.Business.Interfaces
{
    public interface IRule1BO
    {
        Task<GenericReturn> GetRule1Info(AdminRule1Parameters parameters);

        Task<GenericReturn> UpdateAllScores();

        Task<GenericReturn> UpdateScore(string ticker);

    }
}
