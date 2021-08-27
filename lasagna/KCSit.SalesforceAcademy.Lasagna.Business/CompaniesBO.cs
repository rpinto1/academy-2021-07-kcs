﻿using System;
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
       
        public async Task<List<Index>> GetIndex()
        {
            var indexList = _genericDao.GetAllAsync<Index>();
            return await indexList;
        }
        public async Task<GenericReturn<List<CompanyPoco>>> GetCompaniesNamesTickers(string companiesNamesTickers)
        {

            return await _genericBusiness.GenericTransaction(

            async () =>
            {

                return await _searchDao.SearchCompaniesBySearchBar(companiesNamesTickers);
            }

            );
        }
        public async Task<GenericReturn<List<Industry>>> GetIndustries(string sectorName)
        {
            if (sectorName.Equals("All")) 
            {
                sectorName = "";
            }

            return await _genericBusiness.GenericTransaction(

            async () =>
            {

                return await _searchDao.SearchIndustiesBySector(sectorName);
            }

            );
        }

    }
}
