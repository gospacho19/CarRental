using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.CodeAnalysis;


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



        /// <summary>
        /// Creates a new Rental for the given customer, car, and date range.
        /// </summary>
        [SetsRequiredMembers]
        public Rental(Customer customer, Car car, DateRange period, decimal totalCost)
        {
            Customer = customer;
            CustomerId = customer.Id;
            Car = car;
            CarId = car.Id;
            StartDate = period.Start;
            EndDate = period.End;
            TotalCost = totalCost;
            Status = RentalStatus.Booked;
        }




    }
}