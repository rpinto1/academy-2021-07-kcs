using System;
using System.Collections.Generic;

#nullable disable

namespace KCSit.SalesforceAcademy.Kappify.Data
{
    public partial class OrderItem
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int SongId { get; set; }

        public virtual Order Order { get; set; }
        public virtual Song Song { get; set; }
    }
}
