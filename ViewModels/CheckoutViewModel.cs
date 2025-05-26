using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace LuxuryCarRental.ViewModels
{
    public class CheckoutViewModel : ObservableObject
    {
        private string _cardNumber = string.Empty;
        public string CardNumber
        {
            get => _cardNumber;
            set => SetProperty(ref _cardNumber, value);
        }

        private string _expiry = string.Empty;
        public string Expiry
        {
            get => _expiry;
            set => SetProperty(ref _expiry, value);
        }

        private string _cvv = string.Empty;
        public string Cvv
        {
            get => _cvv;
            set => SetProperty(ref _cvv, value);
        }

        public IRelayCommand PayCommand { get; }

        public CheckoutViewModel()
        {
            PayCommand = new RelayCommand(OnPay);
        }

        private void OnPay()
        {
            // TODO: integrate payment processing
        }
    }
}
