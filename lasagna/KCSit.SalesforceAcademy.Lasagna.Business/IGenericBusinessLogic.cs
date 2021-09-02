using System;
using System.Threading.Tasks;

namespace KCSit.SalesforceAcademy.Lasagna.Business
{
    public interface IGenericBusinessLogic
    {
        Task<GenericReturn> GenericTransaction(Func<Task> func);

        Task<GenericReturn<T>> GenericTransaction<T>(Func<Task<T>> func);


        Task<GenericReturn<T>> ExecuteOperation<T>(Func<Task<T>> func);

        GenericReturn<T> ExecuteOperation<T>(Func<T> func);

    }
}