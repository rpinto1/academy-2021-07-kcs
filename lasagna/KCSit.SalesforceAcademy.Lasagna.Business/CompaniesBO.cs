using System;
using System.Collections.Generic;
using System.Text;
using KCSit.SalesforceAcademy.Lasagna.Data;
using Index = KCSit.SalesforceAcademy.Lasagna.Data.Index;
using System.Threading.Tasks;
using KCSit.SalesforceAcademy.Lasagna.Business.Interfaces;
using KCSit.SalesforceAcademy.Lasagna.DataAccess.Interfaces;

namespace KCSit.SalesforceAcademy.Lasagna.Business
{
    public class CompaniesBO : ICompaniesBO
    {

        private ISearchDAO _searchDao;
        private IGenericDAO _genericDao;

        public CompaniesBO(ISearchDAO searchDao, IGenericDAO genericDao)
        {
            _searchDao = searchDao;
            _genericDao = genericDao;
        }
       
        public async Task<List<Index>> GetIndex()
        {
            var indexList = _genericDao.GetAllAsync<Index>();
            return await indexList;
        }
        public string GetIndustries(string? sectorName)
        {
            return "";
        }
        public string GetSectors()
        {
            return "";
        }
    }
}
