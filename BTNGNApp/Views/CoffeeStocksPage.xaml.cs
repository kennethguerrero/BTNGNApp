
using Xamarin.Forms;

namespace BTNGNApp.Views
{
    public partial class CoffeeStocksPage : ContentPage
    {
        public CoffeeStocksPage()
        {
            InitializeComponent();
            BindingContext = ServiceLocator.GetCoffeeStocksViewModel();
        }
    }
}
