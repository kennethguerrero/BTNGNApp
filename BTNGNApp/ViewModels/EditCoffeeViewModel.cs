using System;
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
    [QueryProperty(nameof(CoffeeRowKey), nameof(CoffeeRowKey))]
    public class EditCoffeeViewModel : BaseViewModel
    {
        public Command SaveCommand { get; }
        public Command CancelCommand { get; }
        private readonly ICoffeeManager _coffeeManager;
        private readonly IShellHelper _shellHelper;

        public EditCoffeeViewModel(ICoffeeManager coffeeManager, IShellHelper shellHelper)
        {
            Title = "Edit Coffee";
            SaveCommand = new Command(async () => await Save());
            CancelCommand = new Command(async () => await Cancel());
            _coffeeManager = coffeeManager;
            _shellHelper = shellHelper;
        }

        public async Task Save()
        {
            try
            {
                var selectedCoffee = new Coffee
                {
                    PartitionKey = PartitionKey,
                    RowKey = RowKey,
                    Type = CoffeeType,
                    Weight = CoffeeWeight,
                    MultipliedWeight = MultipliedWeight,
                    Quantity = CoffeeQuantity,
                    IsStock = IsStock,
                    SoldTo = SoldTo,
                    Timestamp = TimeStamp
                };

                var message = await _coffeeManager.Update(selectedCoffee);
                await _shellHelper.DisplayAlert(message);
                await _shellHelper.GotoAsync("..");
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

        public async Task Cancel()
        {
            await _shellHelper.GotoAsync("..");
        }

        string coffeeRowKey;
        public string CoffeeRowKey
        {
            get { return coffeeRowKey; }
            set
            {
                SetProperty(ref coffeeRowKey, value, onChanged: async () => await LoadRowKey(coffeeRowKey));
            }
        }

        public async Task LoadRowKey(string coffeeRowKey)
        {
            try
            {
                var coffee = await _coffeeManager.GetCoffee(coffeeRowKey);
                PartitionKey = coffee.PartitionKey;
                RowKey = coffee.RowKey;
                CoffeeType = coffee.CoffeeType;
                CoffeeWeight = coffee.Weight;
                CoffeeQuantity = coffee.Quantity;
                IsStock = coffee.IsStock;
                SoldTo = coffee.SoldTo;
                TimeStamp = coffee.Timestamp;
            }
            catch (NoInternetException)
            {
                await _shellHelper.DisplayAlert("No Internet Connection");
                await _shellHelper.GotoAsync($"{nameof(CoffeeListPage)}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        public string RowKey { get; set; }
        public string PartitionKey { get; set; }
        public int MultipliedWeight { get; set; }

        string coffeeType;
        public string CoffeeType
        {
            get { return coffeeType; }
            set
            {
                SetProperty(ref coffeeType, value);
            }
        }

        int coffeeWeight;
        public int CoffeeWeight
        {
            get { return coffeeWeight; }
            set
            {
                SetProperty(ref coffeeWeight, value);
                MultiplyWeightToQuantity();
            }
        }

        int coffeeQuantity;
        public int CoffeeQuantity
        {
            get { return coffeeQuantity; }
            set
            {
                SetProperty(ref coffeeQuantity, value);
                MultiplyWeightToQuantity();
            }
        }

        void MultiplyWeightToQuantity()
        {
            MultipliedWeight = CoffeeWeight * CoffeeQuantity;
        }

        bool isStock;
        public bool IsStock
        {
            get { return isStock; }
            set
            {
                SetProperty(ref isStock, value);
            }
        }

        string soldTo;
        public string SoldTo
        {
            get { return soldTo; }
            set
            {
                SetProperty(ref soldTo, value);
            }
        }

        DateTime timeStamp;
        public DateTime TimeStamp
        {
            get { return timeStamp; }
            set
            {
                SetProperty(ref timeStamp, value);
            }
        }
    }
}
