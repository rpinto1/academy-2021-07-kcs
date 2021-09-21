using KCSit.SalesforceAcademy.Lasagna.Data;
using KCSit.SalesforceAcademy.Lasagna.Data.Pocos;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KCSit.SalesforceAcademy.Lasagna.DataAccess.Interfaces
{
    public interface ISearchDAO
    {
        int Get(string ticker);

        Task<int> GetAsync(string ticker);

        Industry GetIndustry(string name);
        // SubIndustry GetSub(string name);
        Task<List<Industry>> SearchIndustiesBySector(string sectorName);
        Task<CompanyScorePoco> SearchCompaniesByIndex(string indexName, string sectorName, string industryName, int page, List<string> countries);

        Task<List<CompanyPoco>> SearchCompaniesBySearchQuery(string search, int pageSize, int pageNumber);
        Task<List<CompanyData>> SearchCompaniesPrice(bool down, List<string> countries);







        Task<List<PortfolioPoco>> GetPortfolios(Guid userId);

        Task<List<PortfolioCompanyPoco>> GetCompaniesByPortfolio(Guid portfolioId);

        Task<int> GetPortfolioId(Guid portfolioUuid);

        void DeletePortfolioId(Guid Uuid);

        void UpdatePortfolioId(Guid Uuid, List<string> Tickers, String PortfolioName);
        Task<CompanyScorePoco> SearchCompaniesByIndexAuthenticated(string indexName, string sectorName, string industryName, int page, List<string> countries);
    }
}