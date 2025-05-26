using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LuxuryCarRental.Handlers.Interfaces;
using LuxuryCarRental.Services.Interfaces;

namespace LuxuryCarRental.ViewModels
{
    public class CheckoutViewModel : ObservableObject
    {
        private readonly ICartService _cart;
        private readonly IPaymentService _payments;
        private readonly ICheckoutHandler _checkoutHandler;

        public CheckoutViewModel(
            ICartService cart,
            IPaymentService payments,
            ICheckoutHandler checkoutHandler)
        {
            _cart = cart;
            _payments = payments;
            _checkoutHandler = checkoutHandler;
            PayCommand = new RelayCommand(OnPay);
        }

        public IRelayCommand PayCommand { get; }

        // bind textboxes to these properties
        public string CardNumber { get; set; } = string.Empty;
        public string Expiry { get; set; } = string.Empty;
        public string Cvv { get; set; } = string.Empty;

        private void OnPay()
        {
            var total = _cart.GetCartTotal(1);
            var txId = _payments.Charge(1, total, /*token*/"tok_test");
            // perform the booking:
            _checkoutHandler.Checkout(1, txId);
            // navigate to Confirmation…
        }
    }

}
