using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuxuryCarRental.Models
{
    public class Rental
    {
        protected Rental() { } 

        public int Id { get; set; }
        public required int CustomerId { get; init; }
        public required Customer Customer { get; init; }

        public required int CarId { get; init; }
        public required Car Car { get; init; }

        public required DateTime StartDate { get; init; }
        public required DateTime EndDate { get; init; }

        public required decimal TotalCost { get; init; }
        public RentalStatus Status { get; set; } = RentalStatus.Booked;
    }
}