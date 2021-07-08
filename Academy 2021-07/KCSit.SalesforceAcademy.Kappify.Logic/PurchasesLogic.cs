using KCSit.SalesforceAcademy.Kappify.Data;
using KCSit.SalesforceAcademy.Kappify.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KCSit.SalesforceAcademy.Kappify.Logic
{
    public class PurchasesLogic
    {
        public ReturnOrderMessageLogic Purchase(Guid customerId)
        {
            var genericDao = new GenericDAO();
            var purchaseDao = new PurchasesDAO();
            var result = new ReturnOrderMessageLogic();
            Customer customer = null;
            try
            {
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

                //abordagem num 1
                //var order = new Order();

                //foreach (var item in cartItems)
                //    order.OrderItems.Add(new OrderItem { });

                //genericDao.Add(order);

                //abordagem 2
                var totalPrice = cartItems.Sum(c => c.songPrice);

                var order = new Order() { Total = totalPrice, CustomerId = customer.Id, DateOfOrder = DateTime.UtcNow };

                genericDao.Add(order);

                var orderItems = new List<OrderItem>();

                foreach (var item in cartItems)
                    orderItems.Add(new OrderItem { SongId = item.cart.SongId , OrderId = order.Id});

         
                genericDao.AddRange(orderItems);

                genericDao.DeleteRange(cartItems.Select(r=> r.cart).ToList());

                result.Order = order;
                result.Message = "Order Successful...";

                //1
                //adicionas os orderitems à order
                //adicionas ao context
                //save changes

                //2
                //crias lista de orderitems
                //adcionas a order ao context
                //adcionas os order items ao context
                //save changes


            }
            catch (Exception e)
            {
                result.Order = null;
                result.Message = e.Message;
            }
            
            return result;
            
        }
    }
}
