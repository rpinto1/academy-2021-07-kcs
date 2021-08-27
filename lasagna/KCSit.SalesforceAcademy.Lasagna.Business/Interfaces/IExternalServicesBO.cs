using RestSharp;
using System;

namespace KCSit.SalesforceAcademy.Lasagna.Business.Interfaces
{
    public interface IExternalServicesBO
    {
        GenericReturn<string> FetchGainLoseData();


    }
}