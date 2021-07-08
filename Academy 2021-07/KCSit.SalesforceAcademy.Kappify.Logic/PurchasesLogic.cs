﻿using KCSit.SalesforceAcademy.Kappify.Data;
using KCSit.SalesforceAcademy.Kappify.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;

namespace KCSit.SalesforceAcademy.Kappify.Logic
{
    public class PurchasesLogic
    {
        public GenericReturn<Order> Purchase(Guid customerId)
        {
            var transaction = new GenericBusinessLogic();

            var result = transaction.GenericTransaction(() =>
            {
                var genericDao = new GenericDAO();
                var purchaseDao = new PurchasesDAO();
                Customer customer = null;
                customer = genericDao.Get<Customer>(customerId);

                if (customer == null)
                {
                    throw new Exception("Customer not found...");
                }


                var cartItems = purchaseDao.GetCartItems(customerId);

                if (cartItems.Count == 0)
                {
                    throw new Exception("Cart empty...");
                }

                var totalPrice = cartItems.Sum(c => c.songPrice);

                var order = new Order() { Total = totalPrice, CustomerId = customer.Id, DateOfOrder = DateTime.UtcNow };

                genericDao.Add(order);

                var orderItems = new List<OrderItem>();

                foreach (var item in cartItems)
                    orderItems.Add(new OrderItem { SongId = item.cart.SongId, OrderId = order.Id });


                genericDao.AddRange(orderItems);

                genericDao.DeleteRange(cartItems.Select(r => r.cart).ToList());

                return order;

            });

            return result;

        }
        
    }
}