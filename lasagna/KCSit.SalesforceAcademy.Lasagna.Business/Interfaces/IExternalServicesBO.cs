using RestSharp;
using System;
using System.Threading.Tasks;

namespace KCSit.SalesforceAcademy.Lasagna.Business.Interfaces
{
    public interface IExternalServicesBO
    {
        Task<GenericReturn<string>> FetchGainLoseData();

        Task<GenericReturn<string>> FetchNewsData();

    }
}