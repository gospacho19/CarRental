using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuxuryCarRental.Models
{
    public class Basket
    {
        protected Basket() { } 

        public int Id { get; set; }
        public required int CustomerId { get; init; }
        public required Customer Customer { get; init; }

        public ICollection<CartItem> Items { get; init; } = new List<CartItem>();
    }
}