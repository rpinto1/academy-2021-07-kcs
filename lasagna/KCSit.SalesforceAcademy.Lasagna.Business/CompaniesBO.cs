using System;
using System.Collections.Generic;
using System.Text;
using KCSit.SalesforceAcademy.Lasagna.Data;
using Index = KCSit.SalesforceAcademy.Lasagna.Data.Index;
using System.Threading.Tasks;
using KCSit.SalesforceAcademy.Lasagna.Business.Interfaces;
using KCSit.SalesforceAcademy.Lasagna.DataAccess.Interfaces;
using KCSit.SalesforceAcademy.Lasagna.Data.Pocos;

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

        public async Task<GenericReturn<CompanyScorePoco>> GetCompanyByIIS(string sectorName, string indexName, string industryName, int page)
        {


            return await _genericBusiness.GenericTransaction(

            async () =>
            {

                return await _searchDao.SearchCompaniesByIndex(indexName,sectorName,industryName, page);
            }

            );
        }

    }
}
