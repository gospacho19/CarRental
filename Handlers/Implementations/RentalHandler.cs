using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LuxuryCarRental.Data;
using LuxuryCarRental.Handlers.Interfaces;
using LuxuryCarRental.Models;
using LuxuryCarRental.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LuxuryCarRental.Handlers.Implementations
{
    public class RentalHandler : IRentalHandler
    {
        private readonly AppDbContext _ctx;
        private readonly IPricingService _pricing;
        private readonly IAvailabilityService _availability;

        public RentalHandler(
            AppDbContext ctx,
            IPricingService pricing,
            IAvailabilityService availability)
        {
            _ctx = ctx;
            _pricing = pricing;
            _availability = availability;
        }

        public Rental BookCar(int customerId, int carId, DateRange period, IEnumerable<string> options)
        {
            if (!_availability.IsAvailable(carId, period))
                throw new InvalidOperationException("Car not available.");

            // 1) Load the domain objects
            var customer = _ctx.Customers.Find(customerId)
                         ?? throw new InvalidOperationException("Customer not found.");
            var car = _ctx.Cars.Find(carId)
                     ?? throw new InvalidOperationException("Car not found.");

            // 2) Calculate the price
            var cost = _pricing.CalculateTotal(car, period, options);

            // 3) Create the Rental via the public constructor
            // new 4-arg constructor
            var rental = new Rental(customer, car, period, cost.Amount);


            // 4) Persist
            _ctx.Rentals.Add(rental);
            _ctx.SaveChanges();

            return rental;
        }


        public void StartRental(int rentalId)
        {
            var r = _ctx.Rentals.Find(rentalId) ?? throw new ArgumentException("Not found");
            r.Status = RentalStatus.Active;
            _ctx.SaveChanges();
        }

        public void CompleteRental(int rentalId)
        {
            var r = _ctx.Rentals.Find(rentalId) ?? throw new ArgumentException("Not found");
            r.Status = RentalStatus.Completed;
            _ctx.SaveChanges();
        }

        public void CancelRental(int rentalId)
        {
            var r = _ctx.Rentals.Find(rentalId) ?? throw new ArgumentException("Not found");
            r.Status = RentalStatus.Cancelled;
            _ctx.SaveChanges();
        }

        public IEnumerable<Rental> GetAllDeals()
            => _ctx.Rentals
                   .Include(r => r.Car)
                   .Include(r => r.Customer)
                   .ToList();
    }
}
