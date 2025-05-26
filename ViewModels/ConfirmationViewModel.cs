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
        private string _message = string.Empty;
        public string Message
        {
            get => _message;
            set => SetProperty(ref _message, value);
        }

        public ConfirmationViewModel()
        {
            Message = "Your booking has been confirmed!";
        }
    }
}