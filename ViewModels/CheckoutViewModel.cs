using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LuxuryCarRental.Handlers.Interfaces;
using LuxuryCarRental.Services.Interfaces;
using CommunityToolkit.Mvvm.Messaging;
using LuxuryCarRental.Messaging;


namespace LuxuryCarRental.ViewModels
{
    public class CheckoutViewModel : ObservableObject
    {
        private readonly ICartService _cart;
        private readonly IPaymentService _payments;
        private readonly ICheckoutHandler _checkoutHandler;
        private readonly IMessenger _messenger;

        public CheckoutViewModel(
            ICartService cart,
            IPaymentService payments,
            ICheckoutHandler checkoutHandler,
            IMessenger messenger)

        {
            _cart = cart;
            _payments = payments;
            _checkoutHandler = checkoutHandler;
            _messenger = messenger;
            PayCommand = new RelayCommand(OnPay);
        }

        public IRelayCommand PayCommand { get; }

        // bind textboxes to these properties
        public string CardNumber { get; set; } = string.Empty;
        public string Expiry { get; set; } = string.Empty;
        public string Cvv { get; set; } = string.Empty;

        private void OnPay()
        {
            // 1) Compute total for customer #1 (hard-coded for demo)
            var total = _cart.GetCartTotal(1);

            // 2) Charge the card — you can build a token string from the fields, or pass CardNumber directly
            var paymentToken = $"{CardNumber}|{Expiry}|{Cvv}";
            var transactionId = _payments.Charge(1, total, paymentToken);

            // 3) Create the rentals
            var rentals = _checkoutHandler.Checkout(1, transactionId);

            // 4) Optionally clear the form
            CardNumber = Expiry = Cvv = string.Empty;
            OnPropertyChanged(nameof(CardNumber));
            OnPropertyChanged(nameof(Expiry));
            OnPropertyChanged(nameof(Cvv));

            // 5) Navigate to the confirmation screen
            _messenger.Send(new GoToConfirmationMessage());
        }

    }

}
