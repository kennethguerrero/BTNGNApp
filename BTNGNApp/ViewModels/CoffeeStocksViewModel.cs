using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using BTNGNApp.Exceptions;
using BTNGNApp.Helpers;
using BTNGNApp.Managers;
using BTNGNApp.Models;
using Xamarin.Forms;

namespace BTNGNApp.ViewModels
{
    public class CoffeeStocksViewModel : BaseViewModel
    {
        public ObservableCollection<Coffee> Coffees { get; }
        public Command GetStocksCommand { get; }
        private readonly ICoffeeManager _coffeeManager;
        private readonly IShellHelper _shellHelper;

        public CoffeeStocksViewModel(ICoffeeManager coffeeManager, IShellHelper shellHelper)
        {
            Title = "Coffee Stocks";
            Coffees = new ObservableCollection<Coffee>();
            GetStocksCommand = new Command(async () => await GetStocks());
            _coffeeManager = coffeeManager;
            _shellHelper = shellHelper;
        }

        public async Task GetStocks()
        {
            try
            {
                IsBusy = true;

                if (SelectedCoffeeType == null)
                    return;

                var coffees = await _coffeeManager.GetCoffees();

                var stocks = coffees.Where(c => c.IsStock.Equals(true) && c.CoffeeType.Equals(SelectedCoffeeType.Option));
                var sales = coffees.Where(c => c.IsStock.Equals(false) && c.CoffeeType.Equals(SelectedCoffeeType.Option));

                var totalStocks = stocks.Sum(s => s.MultipliedWeight);
                var totalSales = sales.Sum(s => s.MultipliedWeight);

                RemainingStocks = totalStocks - totalSales;
            }
            catch (NoInternetException)
            {
                await _shellHelper.DisplayAlert("No Internet Connection");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public IList<CoffeeType> CoffeeTypes { get { return CoffeeTypeData.CoffeeType; } }
        CoffeeType selectedCoffeeType;
        public CoffeeType SelectedCoffeeType
        {
            get => selectedCoffeeType;
            set => SetProperty(ref selectedCoffeeType, value);
        }

        int remainingStocks;
        public int RemainingStocks
        {
            get => remainingStocks;
            set => SetProperty(ref remainingStocks, value);
        }
    }
}
