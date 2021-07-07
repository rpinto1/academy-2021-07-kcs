using System;
using System.Collections.Generic;

#nullable disable

namespace KCSit.SalesforceAcademy.Kappify.Data
{
    public partial class Order
    {
        public Order()
        {
            OrderItems = new HashSet<OrderItem>();
        }

        public int Id { get; set; }
        public decimal Total { get; set; }
        public DateTime DateOfOrder { get; set; }
        public int CustomerId { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}
