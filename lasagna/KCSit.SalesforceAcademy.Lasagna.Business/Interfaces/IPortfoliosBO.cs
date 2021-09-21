using KCSit.SalesforceAcademy.Lasagna.Data.Pocos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KCSit.SalesforceAcademy.Lasagna.Business.Interfaces
{
    public interface IPortfoliosBO
    {
        Task<GenericReturn<List<PortfolioPoco>>> GetPortfolios(Guid userId);

        Task<GenericReturn<List<PortfolioCompanyPoco>>> GetCompaniesByPortfolio(Guid portfolioId);

        Task<GenericReturn<PortfolioPoco>> GetPortfolioWithCompanies(Guid portfolioId);

        Task<GenericReturn<List<PortfolioCompanyValuesPoco>>> GetCompanyValuesByTicker(string ticker);

        Task<GenericReturn> CreatePortfolio(string userId, string portfolioName);

        Task<GenericReturn> AddCompanyToPortfolio(Guid portfolioId, string ticker);

        Task<GenericReturn<List<PortfolioCompanyPoco>>> GetPortfolio(Guid Id);    //RAúl

        void DeletePortfolio(Guid Id);

        void UpdatePortfolioId(Guid Uuid, List<string> Tickers, String PortfolioName);
    }
}
