
using Xamarin.Forms;

namespace BTNGNApp.Views
{
    public partial class CoffeeListPage : ContentPage
    {
        public CoffeeListPage()
        {
            InitializeComponent();
            BindingContext = ServiceLocator.GetCoffeeListViewModel();
        }
    }
}
