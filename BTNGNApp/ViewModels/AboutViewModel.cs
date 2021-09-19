using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace BTNGNApp.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public Command OpenWebsiteCommand { get; }

        public AboutViewModel()
        {
            Title = "About";
            OpenWebsiteCommand = new Command(async () => await OpenWebsite());
        }

        public async Task OpenWebsite()
        {
            await Browser.OpenAsync("https://btngn.com");
        }
    }
}
