using KCSit.SalesforceAcademy.Lasagna.Business.Pocos;
using RestSharp;
using System;
using System.Threading.Tasks;

namespace KCSit.SalesforceAcademy.Lasagna.Business.Interfaces
{
    public interface IExternalServicesBO
    {
        Task<GenericReturn<GainLosePoco>> FetchGainLoseData();

        Task<GenericReturn<NewsPoco>> FetchNewsData();

    }
}