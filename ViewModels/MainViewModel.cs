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
    public class MainViewModel : ObservableObject
    {
        public ObservableCollection<Car> Cars { get; } = new();

        public IRelayCommand RefreshCommand { get; }

        private readonly IUnitOfWork _uow;

        public MainViewModel(IUnitOfWork uow)
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