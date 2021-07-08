using KCSit.SalesforceAcademy.Kappify.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KCSit.SalesforceAcademy.Kappify.DataAccess
{
    public class PurchasesDAO
    {
        public List<CartItem> GetCartItems(Guid customerId)
        {
            using (var context = new academykcsContext())
            {
                return (from cartItem in context.CartItems
                             join customer in context.Customers
                             on cartItem.CustomerId equals customer.Id
                             where customer.Uuid == customerId
                             select cartItem
                             ).ToList();
            }
        }
    }
}
