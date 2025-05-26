using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Models/CartItem.cs
using System.Diagnostics.CodeAnalysis;

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

        [SetsRequiredMembers]
        public CartItem(Basket basket, Car car, DateRange period, IEnumerable<string> options)
        {
            // 1) Link to basket
            BasketId = basket.Id;
            Basket = basket;

            // 2) Link to car
            CarId = car.Id;
            Car = car;

            // 3) Dates
            StartDate = period.Start;
            EndDate = period.End;

            // 4) Compute subtotal (example: daily rate × days)
            var days = period.Days;
            Subtotal = car.DailyRate * days;
            // (You could integrate IPricingService here if you prefer.)
        }
    }
}
