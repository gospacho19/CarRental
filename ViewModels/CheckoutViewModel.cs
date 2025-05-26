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
using LuxuryCarRental.Models;


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
            const int demoCustomerId = 1;

            // 1) Gather items & total
            var items = _cart.GetCartItems(demoCustomerId);
            var total = _cart.GetCartTotal(demoCustomerId);

            // 2) Create card object
            var expiryParts = Expiry.Split('/');
            var card = new Card
            {
                CustomerId = demoCustomerId,
                CardNumber = CardNumber,
                ExpiryMonth = int.Parse(expiryParts[0]),
                ExpiryYear = int.Parse(expiryParts[1]),
                Cvv = Cvv
            };
            // Optionally persist the card:
            // _cardService.AddCard(card);

            // 3) Charge via new signature
            var transactionId = _payments.Charge(card, total);

            // 4) Checkout handler
            _checkoutHandler.Checkout(demoCustomerId, transactionId);

            // 5) Navigate with full payload
            _messenger.Send(new GoToConfirmationMessage(total, items, card));

            // 6) Clear form
            CardNumber = Expiry = Cvv = string.Empty;
            OnPropertyChanged(nameof(CardNumber));
            OnPropertyChanged(nameof(Expiry));
            OnPropertyChanged(nameof(Cvv));
        }


    }

}
