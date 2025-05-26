using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LuxuryCarRental.Models;
using LuxuryCarRental.Repositories.Interfaces;

namespace LuxuryCarRental.ViewModels
{
    public class CartViewModel : ObservableObject
    {
        private readonly IUnitOfWork _uow;

        public ObservableCollection<CartItem> Items { get; } = new();
        public decimal Total { get; private set; }

        public IRelayCommand RefreshCommand { get; }
        public IRelayCommand<CartItem?> RemoveCommand { get; }
        public IRelayCommand ClearCommand { get; }
        public IRelayCommand CheckoutCommand { get; }

        public CartViewModel(IUnitOfWork uow)
        {
            _uow = uow;

            RefreshCommand = new RelayCommand(Refresh);
            RemoveCommand = new RelayCommand<CartItem?>(OnRemove);
            ClearCommand = new RelayCommand(OnClear);
            CheckoutCommand = new RelayCommand(OnCheckout);

            Refresh();
        }

        private void Refresh()
        {
            Items.Clear();
            foreach (var item in _uow.CartItems.GetAll())
                Items.Add(item);

            Total = 0m;
            foreach (var i in Items)
                Total += i.Subtotal;
            OnPropertyChanged(nameof(Total));
        }

        private void OnRemove(CartItem? item)
        {
            if (item == null) return;
            _uow.CartItems.Remove(item);
            _uow.Commit();
            Refresh();
        }

        private void OnClear()
        {
            foreach (var item in Items)
                _uow.CartItems.Remove(item);
            _uow.Commit();
            Refresh();
        }

        private void OnCheckout()
        {
            // TODO: call manager/service to create rentals
            // Clear cart after
            foreach (var item in Items)
                _uow.CartItems.Remove(item);
            _uow.Commit();
            Refresh();
        }
    }
}