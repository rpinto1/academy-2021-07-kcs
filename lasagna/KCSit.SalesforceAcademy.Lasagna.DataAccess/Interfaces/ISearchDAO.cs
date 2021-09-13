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

        IEnumerable<KeyRatiosPoco> GetKeyRatios(string ticker);

        IEnumerable<KeyRatiosPoco> GetKeyRatiosByBulk(List<string> tickers);

        IEnumerable<BalanceSheetPoco> GetBalanceSheet(string ticker);

        IEnumerable<BalanceSheetPoco> GetBalanceSheetByBulk(List<string> tickers);

        IEnumerable<IncomeStatementPoco> GetIncomeStatement(string ticker);

        IEnumerable<IncomeStatementPoco> GetIncomeStatementByBulk(List<string> tickers);

        DailyInfoPoco GetDailyInfo(string ticker);

        IEnumerable<DailyInfoPoco> GetDailyInfoByBulk(List<string> tickers);

        ScorePoco GetScore(string ticker, int scoringMethodId);

        IEnumerable<ScorePoco> GetScoreByBulk(List<string> tickers, int scoringMethodId);
    }
}