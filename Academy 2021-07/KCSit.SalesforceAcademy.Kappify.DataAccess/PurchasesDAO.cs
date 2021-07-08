using KCSit.SalesforceAcademy.Kappify.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KCSit.SalesforceAcademy.Kappify.DataAccess
{
    public class PurchasesDAO
    {
        public List<(CartItem cart, decimal songPrice)> GetCartItems(Guid customerId)
        {
            using (var context = new academykcsContext())
            {
                var results = (from cartItem in context.CartItems
                             join customer in context.Customers
                             on cartItem.CustomerId equals customer.Id
                             join song in context.Songs
                             on cartItem.SongId equals song.Id
                             where customer.Uuid == customerId
                             select new { CartItem = cartItem, SongPrice = song.Price }
                             ).ToList();
                return results.Select(r => (r.CartItem, r.SongPrice)).ToList();
            }
        }
    }
}
