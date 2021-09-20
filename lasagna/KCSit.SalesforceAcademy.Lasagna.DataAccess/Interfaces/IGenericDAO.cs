
using KCSit.SalesforceAcademy.Lasagna.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KCSit.SalesforceAcademy.Lasagna.DataAccess.Interfaces
{
    public interface IGenericDAO
    {
        T Add<T>(T generic) where T : class;
        Task<T> AddAsync<T>(T generic) where T : class;
        void AddRange<T>(List<T> generic) where T : class;
        Task AddRangeAsync<T>(List<T> generic) where T : class;
        void Delete<T>(T generic) where T : class;
        Task DeleteAsync<T>(T generic) where T : class;
        void DeleteRange<T>(List<T> generic) where T : class;
        Task DeleteRangeAsync<T>(List<T> generic) where T : class;
        T Get<T>(Guid guid) where T : class, IEntity;
        T Get<T>(int id) where T : class;
        List<T> GetAll<T>() where T : class;
        Task<List<T>> GetAllAsync<T>() where T : class;
        Task<T> GetAsync<T>(Guid guid) where T : class, IEntity;
        void Update<T>(T generic) where T : class;
        Task UpdateAsync<T>(T generic) where T : class;
        void UpdateRange<T>(List<T> generic) where T : class;

        Task<int> GetCount<T>() where T : class;
    }
}