using System.Collections.Generic;
using System.Threading.Tasks;
using BTNGNApp.Exceptions;
using BTNGNApp.Helpers;
using BTNGNApp.Models;
using BTNGNApp.Services;

namespace BTNGNApp.Managers
{
    public class CoffeeManager : BaseManager, ICoffeeManager
    {
        private readonly IAzureService _azureService;

        public CoffeeManager(IAzureService azureService, IConnectivityHelper connectivityHelper) : base(connectivityHelper)
        {
            _azureService = azureService;
        }

        public async Task<IEnumerable<Coffee>> GetCoffees()
        {
            if (!CheckInternetConnection())
                throw new NoInternetException();

            var result = await _azureService.GetCoffees();
            return result;
        }

        public async Task<Coffee> GetCoffee(string rowKey)
        {
            if (!CheckInternetConnection())
                throw new NoInternetException();

            var result = await _azureService.GetCoffee(rowKey);
            return result;
        }

        public async Task<string> AddCoffee(Coffee coffee)
        {
            if (!CheckInternetConnection())
                throw new NoInternetException();

            var result = await _azureService.AddCoffee(coffee);
            return result;
        }

        public async Task<string> DeleteCoffee(string partitionKey, string rowKey)
        {
            if (!CheckInternetConnection())
                throw new NoInternetException();

            var result = await _azureService.DeleteCoffee(partitionKey, rowKey);
            return result;
        }

        public async Task<string> Update(Coffee coffee)
        {
            if (!CheckInternetConnection())
                throw new NoInternetException();

            var result = await _azureService.Update(coffee);
            return result;
        }
    }
}
