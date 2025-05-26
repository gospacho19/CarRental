using System;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LuxuryCarRental.Models;
using LuxuryCarRental.Repositories.Interfaces;
using LuxuryCarRental.Services.Interfaces;

namespace LuxuryCarRental.ViewModels
{
    public class CatalogViewModel : ObservableObject
    {
        
        public ObservableCollection<Car> Cars { get; } = new();

        private readonly IUnitOfWork _uow;           // still used for raw fetch
        private readonly IAvailabilityService _availability;
        private readonly IPricingService _pricing;
        private readonly ICartService _cart;

        public CatalogViewModel(
            IUnitOfWork uow,
            IAvailabilityService availability,
            IPricingService pricing,
            ICartService cart)
        {
            _uow = uow;
            _availability = availability;
            _pricing = pricing;
            _cart = cart;

            RefreshCommand = new RelayCommand(Refresh);
            AddToCartCommand = new RelayCommand<Car?>(OnAddToCart);
            Refresh();
        }

        public IRelayCommand RefreshCommand { get; }
        public IRelayCommand<Car?> AddToCartCommand { get; }

        private void Refresh()
        {
            Cars.Clear();
            // example uses a 1-day default; you can replace with real date picker
            var defaultPeriod = new DateRange(DateTime.Today, DateTime.Today.AddDays(1));

            foreach (var car in _uow.Cars.GetAll())
            {
                if (_availability.IsAvailable(car.Id, defaultPeriod))
                    Cars.Add(car);
            }
        }

        private void OnAddToCart(Car? car)
        {
            if (car is null) return;

            var period = new DateRange(DateTime.Today, DateTime.Today.AddDays(1));
            _cart.AddToCart(/*customerId*/1, car, period, new string[0]);
        }
    }

}