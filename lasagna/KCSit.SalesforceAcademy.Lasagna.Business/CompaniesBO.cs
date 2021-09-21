﻿using System;
using System.Collections.Generic;
using System.Text;
using KCSit.SalesforceAcademy.Lasagna.Data;
using Index = KCSit.SalesforceAcademy.Lasagna.Data.Index;
using System.Threading.Tasks;
using KCSit.SalesforceAcademy.Lasagna.Business.Interfaces;
using KCSit.SalesforceAcademy.Lasagna.DataAccess.Interfaces;
using KCSit.SalesforceAcademy.Lasagna.Data.Pocos;
using KCSit.SalesforceAcademy.Lasagna.Business.Pocos;
using System.Linq;
using KCSit.SalesforceAcademy.Lasagna.Data.ViewModels;

namespace KCSit.SalesforceAcademy.Lasagna.Business
{
    public class CompaniesBO : ICompaniesBO
    {

        private ISearchDAO _searchDAO;
        private IGenericDAO _genericDAO;
        private IGenericBusinessLogic _genericBusiness;


        public CompaniesBO(ISearchDAO searchDao, IGenericDAO genericDao, IGenericBusinessLogic genericBusiness)
        {
            _searchDAO = searchDao;
            _genericDAO = genericDao;
            _genericBusiness = genericBusiness;
        }
       

        public async Task<GenericReturn<List<CompanyPoco>>> GetCompaniesNamesTickers(string companiesNamesTickers, int pageNumber)
        {
            var pageSize = 30;
            return await _genericBusiness.GenericTransaction(

            async () =>
            {

                return await _searchDAO.SearchCompaniesBySearchQuery(companiesNamesTickers, pageSize, pageNumber);
            }

            );
        }
        public async Task<GenericReturn<List<Industry>>> GetIndustries(string sectorName)
        {

            return await _genericBusiness.GenericTransaction(

            async () =>
            {

                return await _searchDAO.SearchIndustiesBySector(sectorName);
            }

            );
        }

        public async Task<GenericReturn<CompanyScorePoco>> GetCompanyByIIS(string sectorName, string indexName, string industryName, int page, List<string> countries)
        {


            return await _genericBusiness.GenericTransaction(

            async () =>
            {

                return await _searchDAO.SearchCompaniesByIndex(indexName,sectorName,industryName, page, countries);
            }

            );
        }

        public async Task<GenericReturn<GainLoseDBPoco>> GetTopGainerOrLoser(List<string> countries)
        {

            return await _genericBusiness.GenericTransaction(

            async () =>
            {
                var gainersResponse = await _searchDAO.SearchCompaniesPrice(false, countries);
                var losersResponse = await _searchDAO.SearchCompaniesPrice(true, countries);

                var gainLose = new GainLoseDBPoco { Gainers = gainersResponse, Losers = losersResponse };

                return gainLose;
            }

            );
        }

        public async Task<GenericReturn<List<PortfolioPoco>>> GetPortfolios(Guid userId)
        {

            return await _genericBusiness.GenericTransaction(

            async () =>
            {
                var portfolios = await _searchDAO.GetPortfolios(userId);

                foreach(PortfolioPoco portfolio in portfolios)
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
                List<PortfolioCompanyPoco> companies = (List<PortfolioCompanyPoco>) await _searchDAO.GetCompaniesByPortfolio(portfolioId);

                foreach (PortfolioCompanyPoco company in companies)
                {
                    company.Values = GetCompanyValuesByTicker(company.Ticker).Result.Result;
                }


                return companies;

            });
        }



        public async Task<GenericReturn<List<PortfolioCompanyValuesPoco>>> GetCompanyValuesByTicker(string ticker)
        {

            return await _genericBusiness.GenericTransaction(

            async () =>
            {
                List<PortfolioCompanyValuesPoco> values = (List<PortfolioCompanyValuesPoco>)await _searchDAO.GetCompanyValuesByTicker(ticker);

                return values;

            });

        }

        public async Task<GenericReturn> CreatePortfolio(PortfolioViewModel portfolio)
        {

            return await _genericBusiness.GenericTransaction(

            async () =>
            {
                var toInsert = new Portfolio
                {
                    UserId = portfolio.UserId.ToString(),
                    Name = portfolio.Name,
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

                var newPortfolio = await _searchDAO.GetCompaniesByPortfolio(portfolioId);

                var portfolioCompany = new PortfolioCompany()
                {
                    CompanyId = await _searchDAO.GetAsync(ticker),
                    PortfolioId = await _searchDAO.GetPortfolioId(portfolioId)
                };

                var added = _genericDAO.AddAsync(portfolioCompany);

            });

        }

        //

        //-----------------------------------------------Raúl-----------------------------------

        public async Task<GenericReturn<List<PortfolioCompanyPoco>>> GetPortfolio(Guid Id)
        {
            return await _genericBusiness.GenericTransaction(async () =>
            {
                var result = await _searchDAO.GetCompaniesByPortfolio(Id);
                
                return result;
            });
        }

        public void DeletePortfolio(Guid Id)
        {
          _searchDAO.DeletePortfolioId(Id);
        }

        public void UpdatePortfolioId(Guid Uuid, List<string> Tickers, String PortfolioName)
        {
            _searchDAO.UpdatePortfolioId(Uuid, Tickers, PortfolioName);
        }


            return await _genericBusiness.GenericTransaction(

            async () =>
            {

                return await _searchDAO.SearchCompaniesByIndexAuthenticated(indexName, sectorName, industryName, page, countries);
            }

            );
        }
    }
}
