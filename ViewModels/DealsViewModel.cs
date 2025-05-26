using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LuxuryCarRental.Models;
using LuxuryCarRental.Repositories.Interfaces;

namespace LuxuryCarRental.ViewModels
{
    public class DealsViewModel : ObservableObject
    {
        private readonly IUnitOfWork _uow;
        public ObservableCollection<Rental> Rentals { get; } = new();
        public IRelayCommand RefreshCommand { get; }

        public DealsViewModel(IUnitOfWork uow)
        {
            _uow = uow;
            RefreshCommand = new RelayCommand(Refresh);
            Refresh();
        }

        private void Refresh()
        {
            Rentals.Clear();
            foreach (var r in _uow.Rentals.GetAll())
                Rentals.Add(r);
        }
    }
}
