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
using CommunityToolkit.Mvvm.Messaging;
using LuxuryCarRental.Messaging;


namespace LuxuryCarRental.ViewModels
{
    public class MainViewModel : ObservableObject
    {
        public CatalogViewModel CatalogVM { get; }
        public CategoryViewModel CategoryVM { get; }
        public CartViewModel CartVM { get; }
        public CheckoutViewModel CheckoutVM { get; }
        public ConfirmationViewModel ConfirmVM { get; }
        public DealsViewModel DealsVM { get; }

        private object _currentViewModel = default!;

        public object CurrentViewModel
        {
            get => _currentViewModel;
            set => SetProperty(ref _currentViewModel, value);
        }

        public MainViewModel(
            CatalogViewModel catalog,
            CategoryViewModel category,
            CartViewModel cart,
            CheckoutViewModel checkout,
            ConfirmationViewModel confirm,
            DealsViewModel deals,
            IMessenger messenger)

        {
            CatalogVM = catalog;
            CategoryVM = category;
            CartVM = cart;
            CheckoutVM = checkout;
            ConfirmVM = confirm;
            DealsVM = deals;
            // inside the ctor, after you assign CatalogVM, etc.
            messenger.Register<GoToConfirmationMessage>(this, (r, m) =>
            {
                // when received, switch to ConfirmVM
                CurrentViewModel = ConfirmVM;
            });


            // existing commands:
            ShowCatalogCmd = new RelayCommand(() => CurrentViewModel = CatalogVM);
            ShowCategoryCmd = new RelayCommand(() => CurrentViewModel = CategoryVM);
            ShowCartCmd = new RelayCommand(() => CurrentViewModel = CartVM);
            ShowCheckoutCmd = new RelayCommand(() => CurrentViewModel = CheckoutVM);
            ShowDealsCmd = new RelayCommand(() => CurrentViewModel = DealsVM);

            // NEW: command to show confirmation
            ShowConfirmationCmd = new RelayCommand(() => CurrentViewModel = ConfirmVM);

            // start on Catalog
            CurrentViewModel = CatalogVM;
        }

        // existing command properties:
        public IRelayCommand ShowCatalogCmd { get; }
        public IRelayCommand ShowCategoryCmd { get; }
        public IRelayCommand ShowCartCmd { get; }
        public IRelayCommand ShowCheckoutCmd { get; }
        public IRelayCommand ShowDealsCmd { get; }

        // ← add this:
        public IRelayCommand ShowConfirmationCmd { get; }
    }

}