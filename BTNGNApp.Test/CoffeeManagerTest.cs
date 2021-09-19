using System.Linq;
using System.Threading.Tasks;
using BTNGNApp.Exceptions;
using BTNGNApp.Helpers;
using BTNGNApp.Managers;
using BTNGNApp.Models;
using BTNGNApp.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BTNGNApp.Test
{
    [TestClass]
    public class CoffeeManagerTest
    {
        Mock<IAzureService> azureServiceMock;
        Mock<IConnectivityHelper> connectivityHelperMock;
        CoffeeManager coffeeManager;

        [TestInitialize]
        public void Initialize()
        {
            azureServiceMock = new Mock<IAzureService>();
            connectivityHelperMock = new Mock<IConnectivityHelper>();

            connectivityHelperMock.Setup(m => m.IsConnected).Returns(true);

            coffeeManager = new CoffeeManager(azureServiceMock.Object, connectivityHelperMock.Object);
        }

        [TestMethod]
        public async Task GetCoffees_ReturnsCoffees()
        {
            azureServiceMock.Setup(m => m.GetCoffees())
                .ReturnsAsync(new Coffee[]
                {
                    new Coffee(){ },
                    new Coffee(){ },
                    new Coffee(){ }
                });

            var expected = await coffeeManager.GetCoffees();
            Assert.IsTrue(expected != null);
            Assert.IsTrue(expected.Count() == 3);
        }

        [TestMethod]
        [ExpectedException(typeof(NoInternetException))]
        public async Task GetCoffees_ThrowsException_NoInternet()
        {
            connectivityHelperMock.Setup(m => m.IsConnected).Returns(false);
            await coffeeManager.GetCoffees();
        }

        [TestMethod]
        public async Task GetCoffee_ReturnsCoffee()
        {
            azureServiceMock.Setup(m => m.GetCoffee("r1"))
                .ReturnsAsync(new Coffee()
                {
                    RowKey = "r1",
                    Type = "Test Type"
                });

            var expected = await coffeeManager.GetCoffee("r1");
            Assert.IsTrue(expected.Type.Equals("Test Type"));
        }

        [TestMethod]
        [ExpectedException(typeof(NoInternetException))]
        public async Task GetCoffee_ThrowsException_NoInternet()
        {
            connectivityHelperMock.Setup(m => m.IsConnected).Returns(false);
            await coffeeManager.GetCoffee(It.IsAny<string>());
        }

        [TestMethod]
        public async Task AddCoffee_ReturnsCoffee()
        {
            var coffee = new Coffee()
            {
                Type = "Test Type"
            };

            var expected = $"{coffee.Type} has been added";

            azureServiceMock.Setup(m => m.AddCoffee(coffee))
                .ReturnsAsync(expected);

            var actual = await coffeeManager.AddCoffee(coffee);

            Assert.IsNotNull(actual);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(NoInternetException))]
        public async Task AddCoffee_ThrowsException_NoInternet()
        {
            connectivityHelperMock.Setup(m => m.IsConnected).Returns(false);
            await coffeeManager.AddCoffee(It.IsAny<Coffee>());
        }

        [TestMethod]
        public async Task DeleteCoffee_ReturnsCoffee()
        {
            var expected = "Coffee has been deleted";

            azureServiceMock.Setup(m => m.DeleteCoffee("r1", "p1"))
                .ReturnsAsync(expected);

            var actual = await coffeeManager.DeleteCoffee("r1", "p1");

            Assert.IsNotNull(actual);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(NoInternetException))]
        public async Task DeleteCoffee_ThrowsException_NoInternet()
        {
            connectivityHelperMock.Setup(m => m.IsConnected).Returns(false);
            await coffeeManager.DeleteCoffee(It.IsAny<string>(), It.IsAny<string>());
        }

        [TestMethod]
        public async Task Update_ReturnsCoffee()
        {
            var coffee = new Coffee() { RowKey = "r1" };
            var expected = "Coffee has been updated";

            azureServiceMock.Setup(m => m.Update(coffee))
                .ReturnsAsync(expected);

            var actual = await coffeeManager.Update(coffee);

            Assert.IsNotNull(actual);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(NoInternetException))]
        public async Task Update_ThrowsException_NoInternet()
        {
            connectivityHelperMock.Setup(m => m.IsConnected).Returns(false);
            await coffeeManager.Update(It.IsAny<Coffee>());
        }
    }
}
