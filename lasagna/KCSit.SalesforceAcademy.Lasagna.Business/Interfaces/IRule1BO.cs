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
        Task<GenericReturn> GetRule1Info(string queryString);

        Task<GenericReturn> UpdateOneScore(string ticker);

        Task<GenericReturn<List<string>>> UpdateManyScores(string tickersStr);

        Task<GenericReturn> UpdateAllScores();


    }
}
