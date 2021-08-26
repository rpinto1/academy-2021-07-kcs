using KCSit.SalesforceAcademy.Lasagna.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KCSit.SalesforceAcademy.Lasagna.Business.Interfaces
{
    public interface ICompaniesBO
    {
        //string GetIndex();
        //string GetIndustries(string sectorName);
        //string GetSectors();
        Task<GenericReturn<List<Industry>>> GetIndustries(string? sectorName);
    }
}