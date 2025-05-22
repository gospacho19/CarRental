using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuxuryCarRental.Models
{
    public class CartItem
    {
        protected CartItem() { } 

        public int Id { get; set; }
        public required int BasketId { get; init; }
        public required Basket Basket { get; init; }

        public required int CarId { get; init; }
        public required Car Car { get; init; }

        public required DateTime StartDate { get; init; }
        public required DateTime EndDate { get; init; }

        public required decimal Subtotal { get; init; }
    }
}