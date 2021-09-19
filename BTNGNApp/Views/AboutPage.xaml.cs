using Xamarin.Forms;

namespace BTNGNApp.Views
{
    public partial class AboutPage : ContentPage
    {
        public AboutPage()
        {
            InitializeComponent();
            BindingContext = ServiceLocator.GetAboutViewModel();
        }
    }
}
