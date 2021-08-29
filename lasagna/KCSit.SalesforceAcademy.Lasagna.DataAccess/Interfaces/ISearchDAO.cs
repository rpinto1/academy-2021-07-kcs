using KCSit.SalesforceAcademy.Lasagna.Data;
using KCSit.SalesforceAcademy.Lasagna.Data.Pocos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KCSit.SalesforceAcademy.Lasagna.DataAccess.Interfaces
{
    public interface ISearchDAO
    {
        int Get(string ticker);
        Industry GetIndustry(string name);
        // SubIndustry GetSub(string name);
        Task<List<Industry>> SearchIndustiesBySector(string sectorName);
        Task<CompanyScorePoco> SearchCompaniesByIndex(string indexName, string sectorName, string industryName, int page);

        Task<List<CompanyPoco>> SearchCompaniesBySearchQuery(string search);

    }
}