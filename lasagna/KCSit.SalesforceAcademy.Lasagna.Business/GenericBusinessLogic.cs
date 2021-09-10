using System;
using System.Threading.Tasks;
using System.Transactions;

namespace KCSit.SalesforceAcademy.Lasagna.Business
{

    public class GenericBusinessLogic : IGenericBusinessLogic
    {
        public async Task<GenericReturn<T>> GenericTransaction<T>(Func<Task<T>> func)
        {
            var transactionOptions = new TransactionOptions();
            transactionOptions.IsolationLevel = IsolationLevel.ReadCommitted;
            transactionOptions.Timeout = TimeSpan.FromSeconds(60);
            using (var transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled))
            {

                var result = new GenericReturn<T>();

                try
                {
                    var funcResult = await func();

                    transactionScope.Complete();

                    result.Succeeded = true;
                    result.Result = funcResult;
                }
                catch (Exception e)
                {
                    result.Result = default(T);
                    result.Succeeded = false;
                    result.Message = e.Message;
                }

                return result;
            }

        }


        public async Task<GenericReturn> GenericTransaction(Func<Task> func)
        {
            var transactionOptions = new TransactionOptions();
            transactionOptions.IsolationLevel = IsolationLevel.ReadCommitted;
            transactionOptions.Timeout = TimeSpan.FromSeconds(6000);
            using (var transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled))
            {

                var result = new GenericReturn();

                try
                {
                    await func();

                    transactionScope.Complete();

                    result.Succeeded = true;
                }
                catch (Exception e)
                {
                    result.Succeeded = false;
                    result.Message = e.Message;
                }

                return result;
            }

        }



        
        public async Task<GenericReturn<T>> ExecuteOperation<T>(Func<Task<T>> func)
        {
            var result = new GenericReturn<T>();

            try
            {
                var funcResult = await func();

                result.Succeeded = true;
                result.Result = funcResult;
            }
            catch (Exception e)
            {
                result.Result = default(T);
                result.Succeeded = false;
                result.Message = e.Message;
            }

            return result;
        }

        
        public GenericReturn<T> ExecuteOperation<T>(Func<T> func)
        {
            var result = new GenericReturn<T>();

            try
            {
                var funcResult = func();

                result.Succeeded = true;
                result.Result = funcResult;
            }
            catch (Exception e)
            {
                result.Result = default(T);
                result.Succeeded = false;
                result.Message = e.Message;
            }

            return result;
        }

    }
}
