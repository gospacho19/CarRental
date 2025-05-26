using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using LuxuryCarRental.Views;

namespace LuxuryCarRental;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        NavTabs.SelectionChanged += (s, e) =>
        {
            switch (NavTabs.SelectedIndex)
            {
                case 0: ContentArea.Content = new CatalogView(); break;
                case 1: ContentArea.Content = new CategoryView(); break;
                case 2: ContentArea.Content = new CartView(); break;
                case 3: ContentArea.Content = new CheckoutView(); break;
                case 4: ContentArea.Content = new ConfirmationView(); break;
                case 5: ContentArea.Content = new DealsView(); break;
                default:
                    ContentArea.Content = null;
                    break;
            }
        };

        // show first tab by default
        NavTabs.SelectedIndex = 0;
    }
}