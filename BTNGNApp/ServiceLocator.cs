using BTNGNApp.Helpers;
using BTNGNApp.Managers;
using BTNGNApp.Services;
using BTNGNApp.ViewModels;

namespace BTNGNApp
{
    public class ServiceLocator
    {
        public static IShellHelper ShellHelper = new ShellHelper();
        public static IConnectivityHelper ConnectivityHelper = new ConnectivityHelper();
        public static IAzureService AzureService = new AzureService();
        public static ICoffeeManager CoffeeManager = new CoffeeManager(AzureService, ConnectivityHelper);

        public static CoffeeListViewModel GetCoffeeListViewModel()
        {
            return new CoffeeListViewModel(CoffeeManager, ShellHelper);
        }

        public static CoffeeStocksViewModel GetCoffeeStocksViewModel()
        {
            return new CoffeeStocksViewModel(CoffeeManager, ShellHelper);
        }

        public static AddCoffeeViewModel GetAddCoffeeViewModel()
        {
            return new AddCoffeeViewModel(CoffeeManager, ShellHelper);
        }

        public static EditCoffeeViewModel GetEditCoffeeViewModel()
        {
            return new EditCoffeeViewModel(CoffeeManager, ShellHelper);
        }

        public static AboutViewModel GetAboutViewModel()
        {
            return new AboutViewModel();
        }
    }
}
