using KCSit.SalesforceAcademy.Kappify.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KCSit.SalesforceAcademy.Kappify.Logic
{
    public interface IGenericLogic
    {
        Task<GenericReturn<T>> Add<T>(T entity) where T : class;
        Task<GenericReturn> Delete<T>(T entity) where T : class;
        Task<GenericReturn<T>> Get<T>(Guid uuid) where T : class, IEntity;
        Task<GenericReturn<List<T>>> GetAll<T>() where T : class;
        Task<GenericReturn> Update<T>(T entity) where T : class;
    }
}