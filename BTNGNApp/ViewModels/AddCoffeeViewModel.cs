using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using BTNGNApp.Exceptions;
using BTNGNApp.Helpers;
using BTNGNApp.Managers;
using BTNGNApp.Models;
using Xamarin.Forms;

namespace BTNGNApp.ViewModels
{
    public class AddCoffeeViewModel : BaseViewModel
    {
        public Command SaveCommand { get; }
        public Command CancelCommand { get; }
        private readonly ICoffeeManager _coffeeManager;
        private readonly IShellHelper _shellHelper;

        public AddCoffeeViewModel(ICoffeeManager coffeeManager, IShellHelper shellHelper)
        {
            Title = "Add Coffee";
            SaveCommand = new Command(async () => await Save());
            CancelCommand = new Command(async () => await Cancel());
            _coffeeManager = coffeeManager;
            _shellHelper = shellHelper;
        }

        public async Task Cancel()
        {
            await _shellHelper.GotoAsync("..");
        }

        public async Task Save()
        {
            try
            {
                if (SelectedCoffeeType == null)
                    return;

                var coffee = new Coffee
                {
                    Type = SelectedCoffeeType.Option,
                    Weight = SelectedWeight.Value,
                    Quantity = CoffeeQuantity,
                    MultipliedWeight = MultipliedWeight,
                    IsStock = IsStock,
                    SoldTo = SoldTo
                };

                var message = await _coffeeManager.AddCoffee(coffee);
                await _shellHelper.DisplayAlert(message);
                Clear();
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

        void Clear()
        {
            SelectedCoffeeType = null;
            SelectedWeight = null;
            CoffeeQuantity = 0;
            IsStock = false;
            SoldTo = null;
        }

        public IList<CoffeeType> CoffeeTypes { get { return CoffeeTypeData.CoffeeType; } }
        CoffeeType selectedCoffeeType;
        public CoffeeType SelectedCoffeeType
        {
            get => selectedCoffeeType;
            set => SetProperty(ref selectedCoffeeType, value);
        }

        public IList<CoffeeWeight> CoffeeWeights { get { return CoffeeWeightData.CoffeeWeight; } }
        CoffeeWeight selectedWeight;
        public CoffeeWeight SelectedWeight
        {
            get => selectedWeight;
            set => SetProperty(ref selectedWeight, value);
        }

        bool isStock;
        public bool IsStock
        {
            get => isStock;
            set
            {
                SetProperty(ref isStock, value);
                SetSoldToValue();
            }
        }

        void SetSoldToValue()
        {
            if (IsStock)
            {
                SoldTo = "NA";
            }
            else
            {
                SoldTo = string.Empty;
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
            if (SelectedWeight == null)
                return;

            MultipliedWeight = SelectedWeight.Value * CoffeeQuantity;
        }

        int multipliedWeight;
        public int MultipliedWeight
        {
            get { return multipliedWeight; }
            set
            {
                SetProperty(ref multipliedWeight, value);
            }
        }
    }
}
