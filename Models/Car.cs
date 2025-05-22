using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuxuryCarRental.Models
{
    public class Car
    {
        protected Car() { }  

        public int Id { get; set; }
        public required string Make { get; init; }
        public required string Model { get; init; }
        public required int Year { get; init; }
        public required decimal DailyRate { get; init; }
        public CarStatus Status { get; set; } = CarStatus.Available;
    }
}