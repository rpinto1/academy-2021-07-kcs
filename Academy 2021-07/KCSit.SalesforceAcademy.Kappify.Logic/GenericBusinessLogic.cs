using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;

namespace KCSit.SalesforceAcademy.Kappify.Logic
{
    public class GenericBusinessLogic
    {
        public GenericReturn<T> GenericTransaction<T>(Func<T> func)
        {


            var transactionOptions = new TransactionOptions();
            transactionOptions.IsolationLevel = IsolationLevel.ReadCommitted;
            transactionOptions.Timeout = TimeSpan.FromSeconds(60);
            using (var transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions))
            {

                var result = new GenericReturn<T>();

                try
                {
                    var funcResult = func();

                    transactionScope.Complete();

                    result.Result = funcResult;
                }
                catch (Exception e)
                {
                    result.Result = default(T);
                    result.Message = e.Message;
                }

                return result;
            }

        }
    }
}
