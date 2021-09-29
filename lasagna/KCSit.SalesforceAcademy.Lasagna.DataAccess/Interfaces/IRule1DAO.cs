using KCSit.SalesforceAcademy.Lasagna.Data.Pocos;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KCSit.SalesforceAcademy.Lasagna.DataAccess.Interfaces
{
    public interface IRule1DAO
    {

        Task<IEnumerable> GetCompanies();

        Task<IEnumerable<CompanyPoco>> GetCompaniesByBulk(int skip, int take);
        
        Task<IEnumerable<string>> GetTickersByBulk(int skip, int take);

        Task<IEnumerable<CompanyPoco>> GetCompaniesByTickerList(List<string> tickers);

        Task<IEnumerable<KeyRatiosPoco>> GetKeyRatios(string ticker);

        Task<IEnumerable<KeyRatiosPoco>> GetKeyRatiosByBulk(List<string> tickers);

        Task<IEnumerable<BalanceSheetPoco>> GetBalanceSheet(string ticker);

        Task<IEnumerable<BalanceSheetPoco>> GetBalanceSheetByBulk(List<string> tickers);

        Task<IEnumerable<IncomeStatementPoco>> GetIncomeStatement(string ticker);

        Task<IEnumerable<IncomeStatementPoco>> GetIncomeStatementByBulk(List<string> tickers);

        Task<DailyInfoPoco> GetDailyInfo(string ticker);

        Task<IEnumerable<DailyInfoPoco>> GetDailyInfoByBulk(List<string> tickers);

        Task<ScorePoco> GetScoreId(string ticker, int scoringMethodId);
        Task<double> GetScore(string ticker, int scoringMethodId);

        Task<IEnumerable<ScorePoco>> GetScoreByBulk(List<string> tickers, int scoringMethodId);

        Task<AdminRule1PocoList> GetInfo(string queryString, int scoringMethod);

    }
}
