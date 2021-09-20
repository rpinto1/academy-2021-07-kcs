using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;
using System.Threading;

namespace KCSit.SalesforceAcademy.Lasagna.DataAccess
{
    public static class ExtensionMethods
    {
        public static IQueryable<T> OrderBy<T>(this IQueryable<T> source, string orderByProperty, string asc)
        {
            string command = asc.Equals("Ascending") ? "OrderBy" : "OrderByDescending";

            var type = typeof(T);
            var property = type.GetProperty(orderByProperty);
            var parameter = Expression.Parameter(type, "p");
            var propertyAccess = Expression.MakeMemberAccess(parameter, property);
            var orderByExpression = Expression.Lambda(propertyAccess, parameter);
            var resultExpression = Expression.Call(typeof(Queryable), command, new Type[] { type, property.PropertyType },
                source.Expression, Expression.Quote(orderByExpression));
            
            return source.Provider.CreateQuery<T>(resultExpression);
        }


    }

}
