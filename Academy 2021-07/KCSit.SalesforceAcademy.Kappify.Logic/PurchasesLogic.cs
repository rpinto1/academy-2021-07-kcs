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
                //var order = new Order();
                //var orderItems = new List<OrderItem>();

                //foreach (var item in cartItems)
                //    orderItems.Add(new OrderItem { });

                //genericDao.Add(order);
                //genericDao.AddRange(orderItems);

                



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
