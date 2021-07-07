using System;
using System.Collections.Generic;

#nullable disable

namespace KCSit.SalesforceAcademy.Kappify.Data
{
    public partial class CartItem
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int SongId { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Song Song { get; set; }
    }
}
