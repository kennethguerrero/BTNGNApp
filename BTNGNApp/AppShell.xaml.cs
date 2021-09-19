using System;
using BTNGNApp.Views;
using Xamarin.Forms;

namespace BTNGNApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(CoffeeListPage), typeof(CoffeeListPage));
            Routing.RegisterRoute(nameof(AddCoffeePage), typeof(AddCoffeePage));
            Routing.RegisterRoute(nameof(EditCoffeePage), typeof(EditCoffeePage));
        }

    }
}
