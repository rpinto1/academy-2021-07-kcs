using System;
using System.Collections.Generic;

#nullable disable

namespace KCSit.SalesforceAcademy.Kappify.Data
{
    public partial class Song
    {
        public Song()
        {
            CartItems = new HashSet<CartItem>();
            OrderItems = new HashSet<OrderItem>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int AlbumId { get; set; }
        public int ArtistId { get; set; }
        public TimeSpan Duration { get; set; }
        public Guid Uuid { get; set; }
        public decimal Price { get; set; }

        public virtual Album Album { get; set; }
        public virtual Artist Artist { get; set; }
        public virtual ICollection<CartItem> CartItems { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}
