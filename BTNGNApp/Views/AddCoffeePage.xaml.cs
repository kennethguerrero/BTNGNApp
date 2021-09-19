
using Xamarin.Forms;

namespace BTNGNApp.Views
{
    public partial class AddCoffeePage : ContentPage
    {
        public AddCoffeePage()
        {
            InitializeComponent();
            BindingContext = ServiceLocator.GetAddCoffeeViewModel();
        }
    }
}
