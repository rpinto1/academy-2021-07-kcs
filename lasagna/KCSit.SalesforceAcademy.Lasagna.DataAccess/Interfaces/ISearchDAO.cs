using KCSit.SalesforceAcademy.Lasagna.Data;
using KCSit.SalesforceAcademy.Lasagna.Data.Pocos;
using System.Collections;
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
        Task<CompanyScorePoco> SearchCompaniesByIndex(string indexName, string sectorName, string industryName, int page, List<string> countries);

        Task<List<CompanyPoco>> SearchCompaniesBySearchQuery(string search, int pageSize, int pageNumber);
        Task<List<CompanyData>> SearchCompaniesPrice(bool down, List<string> countries);





        Task<int> GetCompaniesCount();

        Task<IEnumerable> GetCompanies();

        Task<IEnumerable<CompanyPoco>> GetCompaniesByBulk(int skip, int take);

        Task<IEnumerable<KeyRatiosPoco>> GetKeyRatios(string ticker);

        Task<IEnumerable<KeyRatiosPoco>> GetKeyRatiosByBulk(List<string> tickers);

        Task<IEnumerable<BalanceSheetPoco>> GetBalanceSheet(string ticker);

        Task<IEnumerable<BalanceSheetPoco>> GetBalanceSheetByBulk(List<string> tickers);

        Task<IEnumerable<IncomeStatementPoco>> GetIncomeStatement(string ticker);

        Task<IEnumerable<IncomeStatementPoco>> GetIncomeStatementByBulk(List<string> tickers);

        Task<DailyInfoPoco> GetDailyInfo(string ticker);

        Task<IEnumerable<DailyInfoPoco>> GetDailyInfoByBulk(List<string> tickers);

        Task<ScorePoco> GetScore(string ticker, int scoringMethodId);

        Task<IEnumerable<ScorePoco>> GetScoreByBulk(List<string> tickers, int scoringMethodId);
    }
}