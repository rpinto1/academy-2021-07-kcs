
using KCSit.SalesforceAcademy.Lasagna.Business.Interfaces;
using KCSit.SalesforceAcademy.Lasagna.Data.Interfaces;
using KCSit.SalesforceAcademy.Lasagna.DataAccess;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KCSit.SalesforceAcademy.Lasagna.Business

{
    public class GenericLogic : IGenericLogic
    {
        public async Task<GenericReturn<T>> Get<T>(Guid uuid) where T : class, IEntity
        {
            var genericBusinessLogic = new GenericBusinessLogic();

            return await genericBusinessLogic.GenericTransaction(async () =>
            {
                var genericDao = new GenericDAO();
                return await genericDao.GetAsync<T>(uuid);
            });
        }

        public async Task<GenericReturn<List<T>>> GetAll<T>() where T : class
        {
            var genericBusinessLogic = new GenericBusinessLogic();

            return await genericBusinessLogic.GenericTransaction(
            
            async () =>
            {
                var genericDao = new GenericDAO();
                return await genericDao.GetAllAsync<T>();
            }
            
            );
        }

        public async Task<GenericReturn<T>> Add<T>(T entity) where T : class
        {
            var genericBusinessLogic = new GenericBusinessLogic();

            return await genericBusinessLogic.GenericTransaction(async () =>
            {
                var genericDao = new GenericDAO();
                return await genericDao.AddAsync<T>(entity);
            });
        }

        public async Task<GenericReturn> Update<T>(T entity) where T : class
        {
            var genericBusinessLogic = new GenericBusinessLogic();

            return await genericBusinessLogic.GenericTransaction(async () =>
            {
                var genericDao = new GenericDAO();
                await genericDao.UpdateAsync<T>(entity);
            });
        }

        public async Task<GenericReturn> Delete<T>(T entity) where T : class
        {
            var genericBusinessLogic = new GenericBusinessLogic();

            return await genericBusinessLogic.GenericTransaction(async () =>
            {
                var genericDao = new GenericDAO();
                await genericDao.DeleteAsync<T>(entity);
            });
        }
    }
}
