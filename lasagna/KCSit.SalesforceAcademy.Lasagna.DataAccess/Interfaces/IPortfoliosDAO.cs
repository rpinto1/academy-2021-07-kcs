using KCSit.SalesforceAcademy.Lasagna.Data.Pocos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KCSit.SalesforceAcademy.Lasagna.DataAccess.Interfaces
{
    public interface IPortfoliosDAO
    {
        Task<List<PortfolioPoco>> GetPortfolios(Guid userId);

        Task<List<PortfolioCompanyPoco>> GetCompaniesByPortfolio(Guid portfolioId);

        Task<PortfolioPoco> GetPortfolioWithCompanies(Guid portfolioUuid);

        Task<int> GetPortfolioId(Guid portfolioUuid);

        Task DeletePortfolioId(Guid Uuid);

        Task UpdatePortfolioId(Guid Uuid, List<string> Tickers, String PortfolioName);

    }
}
