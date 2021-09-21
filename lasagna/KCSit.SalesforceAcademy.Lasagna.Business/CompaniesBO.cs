using System;
using System.Collections.Generic;
using KCSit.SalesforceAcademy.Lasagna.Data;
using System.Threading.Tasks;
using KCSit.SalesforceAcademy.Lasagna.Business.Interfaces;
using KCSit.SalesforceAcademy.Lasagna.DataAccess.Interfaces;
using KCSit.SalesforceAcademy.Lasagna.Data.Pocos;

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

                return await _searchDAO.SearchCompaniesByIndex(indexName, sectorName, industryName, page, countries);
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



        public async Task<GenericReturn<CompanyScorePoco>> GetCompanyByIISAuthenticated(string sectorName, string indexName, string industryName, int page, List<string> countries)
        {

            return await _genericBusiness.GenericTransaction(

            async () => { return await _searchDAO.SearchCompaniesByIndexAuthenticated(indexName, sectorName, industryName, page, countries); }

            );

        }











}
}
