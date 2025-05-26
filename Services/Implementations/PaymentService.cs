using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LuxuryCarRental.Models;
using LuxuryCarRental.Services.Interfaces;

namespace LuxuryCarRental.Services.Implementations
{
    public class PaymentService : IPaymentService
    {
        public string Charge(int customerId, Money amount, string paymentToken)
        {
            // TODO: Integrate with real gateway. For now, return a GUID.
            return Guid.NewGuid().ToString();
        }

        string IPaymentService.Charge(int customerId, Money amount, string paymentToken)
        {
            throw new NotImplementedException();
        }
    }
}
