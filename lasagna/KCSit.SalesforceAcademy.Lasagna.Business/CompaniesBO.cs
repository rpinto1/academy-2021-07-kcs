using System;
using System.Collections.Generic;
using System.Text;
using KCSit.SalesforceAcademy.Lasagna.Data;
using Index = KCSit.SalesforceAcademy.Lasagna.Data.Index;
using System.Threading.Tasks;
using KCSit.SalesforceAcademy.Lasagna.Business.Interfaces;
using KCSit.SalesforceAcademy.Lasagna.DataAccess.Interfaces;
using KCSit.SalesforceAcademy.Lasagna.Data.Pocos;
using KCSit.SalesforceAcademy.Lasagna.Business.Pocos;

namespace KCSit.SalesforceAcademy.Lasagna.Business
{
    public class CompaniesBO : ICompaniesBO
    {

        private ISearchDAO _searchDao;
        private IGenericDAO _genericDao;
        private IGenericBusinessLogic _genericBusiness;


        public CompaniesBO(ISearchDAO searchDao, IGenericDAO genericDao, IGenericBusinessLogic genericBusiness)
        {
            _searchDao = searchDao;
            _genericDao = genericDao;
            _genericBusiness = genericBusiness;
        }
       

        public async Task<GenericReturn<List<CompanyPoco>>> GetCompaniesNamesTickers(string companiesNamesTickers, int pageNumber)
        {
            var pageSize = 30;
            return await _genericBusiness.GenericTransaction(

            async () =>
            {

                return await _searchDao.SearchCompaniesBySearchQuery(companiesNamesTickers, pageSize, pageNumber);
            }

            );
        }
        public async Task<GenericReturn<List<Industry>>> GetIndustries(string sectorName)
        {

            return await _genericBusiness.GenericTransaction(

            async () =>
            {

                return await _searchDao.SearchIndustiesBySector(sectorName);
            }

            );
        }

        public async Task<GenericReturn<CompanyScorePoco>> GetCompanyByIIS(string sectorName, string indexName, string industryName, int page, List<string> countries)
        {


            return await _genericBusiness.GenericTransaction(

            async () =>
            {

                return await _searchDao.SearchCompaniesByIndex(indexName,sectorName,industryName, page, countries);
            }

            );
        }

        public async Task<GenericReturn<GainLoseDBPoco>> GetTopGainerOrLoser()
        {


            return await _genericBusiness.GenericTransaction(

            async () =>
            {
                var gainersResponse = await _searchDao.SearchCompaniesPrice(false);
                var losersResponse = await _searchDao.SearchCompaniesPrice(true);

                var gainLose = new GainLoseDBPoco { Gainers = gainersResponse, Losers = losersResponse };

                return gainLose;
            }

            );
        }

    }
}
