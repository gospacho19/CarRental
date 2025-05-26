using System;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LuxuryCarRental.Models;
using LuxuryCarRental.Repositories.Interfaces;

namespace LuxuryCarRental.ViewModels
{
    public class CatalogViewModel : ObservableObject
    {
        private readonly IUnitOfWork _uow;

        public ObservableCollection<Car> Cars { get; } = new();
        public IRelayCommand RefreshCommand { get; }

        public CatalogViewModel(IUnitOfWork uow)
        {
            _uow = uow;
            RefreshCommand = new RelayCommand(Refresh);
            Refresh();
        }

        private void Refresh()
        {
            Cars.Clear();
            foreach (var car in _uow.Cars.GetAll())
                Cars.Add(car);
        }
    }
}