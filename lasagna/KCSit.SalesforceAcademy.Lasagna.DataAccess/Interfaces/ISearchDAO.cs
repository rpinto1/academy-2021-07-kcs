using KCSit.SalesforceAcademy.Lasagna.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KCSit.SalesforceAcademy.Lasagna.DataAccess.Interfaces
{
    public interface ISearchDAO
    {
        int Get(string ticker);
        Industry GetIndustry(string name);
        SubIndustry GetSub(string name);
        Task<List<Company>> SearchCompaniesByIndex(string indexName, string sectorName, string industryName);
        List<Industry> SearchIndustiesBySector(string sectorName);
    }
}