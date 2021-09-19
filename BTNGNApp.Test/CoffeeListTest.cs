using System.Threading.Tasks;
using BTNGNApp.Exceptions;
using BTNGNApp.Helpers;
using BTNGNApp.Managers;
using BTNGNApp.Models;
using BTNGNApp.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BTNGNApp.Test
{
    [TestClass]
    public class CoffeeListTest
    {
        Mock<ICoffeeManager> coffeeManagerMock;
        CoffeeListViewModel coffeeListVM;
        Mock<IShellHelper> shellHelperMock;

        [TestInitialize]
        public void Initialize()
        {
            coffeeManagerMock = new Mock<ICoffeeManager>();
            shellHelperMock = new Mock<IShellHelper>();
            coffeeListVM = new CoffeeListViewModel(coffeeManagerMock.Object, shellHelperMock.Object);
        }

        [TestMethod]
        public async Task LoadCoffees_ReturnsCoffees()
        {
            coffeeManagerMock.Setup(m => m.GetCoffees())
                .ReturnsAsync(new Coffee[]
                {
                    new Coffee(){ RowKey = "1"},
                    new Coffee(){ RowKey = "2"},
                    new Coffee(){ RowKey = "3"},
                });
            await coffeeListVM.LoadCoffes();

            var expected = coffeeListVM.LoadCoffes();
            Assert.IsNotNull(expected);

            Assert.IsTrue(coffeeListVM.Coffees.Count == 3);
        }

        [TestMethod]
        public async Task LoadCoffees_ReturnsNoInternetException()
        {
            coffeeManagerMock.Setup(m => m.GetCoffees())
                .Throws<NoInternetException>();

            await coffeeListVM.LoadCoffes();

            shellHelperMock.Verify(m => m.DisplayAlert("No Internet Connection"), Times.Once());
        }

        [TestMethod]
        public async Task AddCoffee_NavigatesToAddCoffeePage()
        {
            await coffeeListVM.AddCoffee();
            shellHelperMock.Verify(m => m.GotoAsync("AddCoffeePage"), Times.Once());
        }

        [TestMethod]
        public async Task EditCoffee_CoffeeNull_GotoAsyncNotCalled()
        {
            await coffeeListVM.EditCoffee(null);
            shellHelperMock.Verify(m => m.GotoAsync(It.IsAny<string>()), Times.Never());
        }

        [TestMethod]
        [DataRow("1")]
        [DataRow("ID-12345")]
        public async Task EditCoffee_CoffeeHasValue(string rowKey)
        {
            await coffeeListVM.EditCoffee(new Coffee { RowKey = rowKey });
            shellHelperMock.Verify(m => m.GotoAsync(It.Is<string>(x => x.Contains(rowKey))), Times.Once());
        }

        [TestMethod]
        public async Task DeleteCoffee_ReturnsNullCoffee_MethodNotCalled()
        {
            await coffeeListVM.DeleteCoffee(null);
            coffeeManagerMock.Verify(m => m.DeleteCoffee(It.IsAny<string>(), It.IsAny<string>()), Times.Never());
        }

        [TestMethod]
        public async Task DeleteCoffee_ReturnsNoInternetException()
        {
            var coffee = new Coffee(){ };

            coffeeManagerMock.Setup(m => m.DeleteCoffee(It.IsAny<string>(), It.IsAny<string>()))
                .Throws<NoInternetException>();

            await coffeeListVM.DeleteCoffee(coffee);
            shellHelperMock.Verify(m => m.DisplayAlert("No Internet Connection"), Times.Once());
        }

        [TestMethod]
        public async Task DeleteCoffee_MethodCalled_ReturnsSpecificDisplayAlert()
        {
            var coffee = new Coffee()
            {
                RowKey = "r1",
                PartitionKey = "p1",
                Type = "Test Type"
            };

            coffeeManagerMock.Setup(m => m.DeleteCoffee(coffee.RowKey, coffee.PartitionKey))
                .ReturnsAsync(It.IsAny<string>());

            await coffeeListVM.DeleteCoffee(coffee);
            shellHelperMock.Verify(m => m.DisplayAlert($"{coffee.Type} has been deleted"), Times.Once());
        }

        [TestMethod]
        public async Task Selected_ReturnsNullCoffee()
        {
            await coffeeListVM.Selected(null);
            shellHelperMock.Verify(m => m.DisplayAlert(It.IsAny<string>()), Times.Never());
        }

        [TestMethod]
        public async Task Selected_ReturnsCoffee_DisplaySpecificDisplayAlert()
        {
            var coffee = new Coffee()
            {
                MultipliedWeight = 1,
                SoldTo = "Test Sold to"
            };

            await coffeeListVM.Selected(coffee);
            shellHelperMock.Verify(m => m.DisplayAlert($"Total Weight: {coffee.MultipliedWeight} g \nSold to: {coffee.SoldTo}"), Times.Once());
        }
    }
}
