using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CommunityToolkit.Mvvm.ComponentModel;

namespace LuxuryCarRental.ViewModels
{
    public class ConfirmationViewModel : ObservableObject
    {
        // you could inject the ICheckoutHandler or the last rentals
        public string Message { get; init; }

        public ConfirmationViewModel()
        {
            Message = "Thank you! Your booking is confirmed.";
        }
    }

}