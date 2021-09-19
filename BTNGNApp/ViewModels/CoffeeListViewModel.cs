using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using BTNGNApp.Exceptions;
using BTNGNApp.Helpers;
using BTNGNApp.Managers;
using BTNGNApp.Models;
using BTNGNApp.Views;
using Xamarin.Forms;

namespace BTNGNApp.ViewModels
{
    public class CoffeeListViewModel : BaseViewModel
    {
        public ObservableCollection<Coffee> Coffees { get; }
        public Command LoadCoffeesCommand { get; }
        public Command AddCommand { get; }
        public Command<Coffee> DeleteCommand { get; }
        public Command<Coffee> EditCommand { get; }
        private readonly ICoffeeManager _coffeeManager;
        private readonly IShellHelper _shellHelper;

        public CoffeeListViewModel(ICoffeeManager coffeeManager, IShellHelper shellHelper)
        {
            Title = "Coffee List";
            Coffees = new ObservableCollection<Coffee>();
            LoadCoffeesCommand = new Command(async () => await LoadCoffes());
            AddCommand = new Command(async () => await AddCoffee());
            DeleteCommand = new Command<Coffee>(async (coffee) => await DeleteCoffee(coffee));
            EditCommand = new Command<Coffee>(async (coffee) => await EditCoffee(coffee));
            _coffeeManager = coffeeManager;
            _shellHelper = shellHelper;
            Initialization = LoadCoffes();
        }

        private Task Initialization { get; set; }
        public async Task LoadCoffes()
        {
            try
            {
                IsBusy = true;
                Coffees.Clear();
                var coffees = await _coffeeManager.GetCoffees();

                foreach(var item in coffees)
                {
                    Coffees.Add(item);
                }
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

        public async Task AddCoffee()
        {
            await _shellHelper.GotoAsync("AddCoffeePage");
        }

        public async Task EditCoffee(Coffee coffee)
        {
            try
            {
                if (coffee == null)
                    return;

                await _shellHelper.GotoAsync($"{nameof(EditCoffeePage)}?{nameof(EditCoffeeViewModel.CoffeeRowKey)}={coffee.RowKey}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        public async Task DeleteCoffee(Coffee coffee)
        {
            try
            {
                if (coffee == null)
                    return;

                await _coffeeManager.DeleteCoffee(coffee.PartitionKey, coffee.RowKey);
                await LoadCoffes();
                await _shellHelper.DisplayAlert($"{coffee.Type} has been deleted");
            }
            catch (NoInternetException)
            {
                await _shellHelper.DisplayAlert("No Internet Connection");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        Coffee selectedCoffee;
        public Coffee SelectedCoffee
        {
            get => selectedCoffee;
            set => SetProperty(ref selectedCoffee, value, onChanged: async () => await Selected(selectedCoffee));
        }

        public async Task Selected(Coffee coffee)
        {
            if (coffee == null)
                return;

            SelectedCoffee = null;
            await _shellHelper.DisplayAlert($"Total Weight: {coffee.MultipliedWeight} g \nSold to: {coffee.SoldTo}");
        }
    }
}
