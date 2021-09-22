using KCSit.SalesforceAcademy.Lasagna.Business.Interfaces;
using KCSit.SalesforceAcademy.Lasagna.Data;
using KCSit.SalesforceAcademy.Lasagna.Data.Pocos;
using KCSit.SalesforceAcademy.Lasagna.DataAccess;
using KCSit.SalesforceAcademy.Lasagna.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KCSit.SalesforceAcademy.Lasagna.Business
{
    public class PortfoliosBO : IPortfoliosBO
    {
        private IPortfoliosDAO _portfoliosDAO;
        private ISearchDAO _searchDAO;
        private IGenericDAO _genericDAO;
        private IGenericBusinessLogic _genericBusiness;

        public PortfoliosBO(IPortfoliosDAO portfoliosDAO, ISearchDAO searchDAO, IGenericDAO genericDAO, IGenericBusinessLogic genericBusiness)
        {
            _portfoliosDAO = portfoliosDAO;
            _searchDAO = searchDAO;
            _genericDAO = genericDAO;
            _genericBusiness = genericBusiness;
        }

        public async Task<GenericReturn<List<PortfolioPoco>>> GetPortfolios(Guid userId)
        {

            return await _genericBusiness.GenericTransaction(

            async () =>
            {
                var portfolios = await _portfoliosDAO.GetPortfolios(userId);

                foreach (PortfolioPoco portfolio in portfolios)
                {
                    portfolio.PortfolioCompanies = GetCompaniesByPortfolio(portfolio.PortfolioId).Result.Result;
                }


                return portfolios;

            });
        }


        public async Task<GenericReturn<List<PortfolioCompanyPoco>>> GetCompaniesByPortfolio(Guid portfolioId)
        {

            return await _genericBusiness.GenericTransaction(

            async () =>
            {
                List<PortfolioCompanyPoco> companies = (List<PortfolioCompanyPoco>)await _portfoliosDAO.GetCompaniesByPortfolio(portfolioId);

                //foreach (PortfolioCompanyPoco company in companies)
                //{
                //    company.Values = GetCompanyValuesByTicker(company.Ticker).Result.Result;
                //}


                return companies;

            });
        }

        public async Task<GenericReturn<PortfolioPoco>> GetPortfolioWithCompanies(Guid portfolioId)
        {

            return await _genericBusiness.GenericTransaction(

            async () =>
            {
                PortfolioPoco portfolio = await _portfoliosDAO.GetPortfolioWithCompanies(portfolioId);

                


                return portfolio;

            });
        }

        public async Task<GenericReturn> CreatePortfolio(string userId, string portfolioName)
        {

            return await _genericBusiness.GenericTransaction(

            async () =>
            {
                var toInsert = new Portfolio
                {
                    UserId = userId,
                    Name = portfolioName,
                    Uuid = new Guid()

                };

                var newPortfolio = await _genericDAO.AddAsync(toInsert);

                return;

            });

        }

        public async Task<GenericReturn> AddCompanyToPortfolio(Guid portfolioId, string ticker)
        {

            return await _genericBusiness.GenericTransaction(

            async () =>
            {

                var newPortfolio = await _portfoliosDAO.GetCompaniesByPortfolio(portfolioId);

                var portfolioCompany = new PortfolioCompany()
                {
                    CompanyId = await _searchDAO.GetAsync(ticker),
                    PortfolioId = await _portfoliosDAO.GetPortfolioId(portfolioId)
                };

                var added = _genericDAO.AddAsync(portfolioCompany);

            });

        }

        public async Task<GenericReturn<List<PortfolioCompanyPoco>>> GetPortfolio(Guid Id)
        {
            return await _genericBusiness.GenericTransaction(async () =>
            {
                var result = await _portfoliosDAO.GetCompaniesByPortfolio(Id);

                return result;
            });
        }

        public void DeletePortfolio(Guid Id)
        {
            _portfoliosDAO.DeletePortfolioId(Id);
        }


        public void UpdatePortfolioId(Guid Uuid, List<string> Tickers, String PortfolioName)
        {
            _portfoliosDAO.UpdatePortfolioId(Uuid, Tickers, PortfolioName);
        }

        public async Task<GenericReturn<List<PortfolioCompanyValuesPoco>>> GetCompanyValuesByTicker(string ticker)
        {

            return await _genericBusiness.GenericTransaction(

            async () =>
            {
                
                var rule1DAO = new Rule1DAO();

                var keyRatiosList = await rule1DAO.GetKeyRatios(ticker);
                var balanceSheetList = await rule1DAO.GetBalanceSheet(ticker);
                var incomeStatementList = await rule1DAO.GetIncomeStatement(ticker);

                var roic = (from kr in keyRatiosList
                            select kr.Roic).ToList();

                var equity = (from bs in balanceSheetList
                              select bs.Equity).ToList();

                var eps = (from incStat in incomeStatementList
                           select incStat.Eps).ToList();

                var sales = (from incStat in incomeStatementList
                             select incStat.Sales).ToList();

                var cash = (from bs in balanceSheetList
                            select bs.Cash).ToList();

                var list = new List<PortfolioCompanyValuesPoco>();


                for (int i = 0; i < keyRatiosList.Count(); i++)
                {
                    list.Add(new PortfolioCompanyValuesPoco
                    {
                        Year = keyRatiosList.ElementAt(i).Year,
                        ROIC = roic.ElementAt(i),
                        Equity = equity.ElementAt(i),
                        EPS = eps.ElementAt(i),
                        Sales = sales.ElementAt(i),
                        Cash = cash.ElementAt(i)
                    });

                }

                return await Task.FromResult(list);

            });

        }
    }
}
