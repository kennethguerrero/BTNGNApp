using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace BTNGNApp.Views
{
    public partial class EditCoffeePage : ContentPage
    {
        public EditCoffeePage()
        {
            InitializeComponent();
            BindingContext = ServiceLocator.GetEditCoffeeViewModel();
        }
    }
}
