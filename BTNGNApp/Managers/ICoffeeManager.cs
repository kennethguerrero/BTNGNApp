using System.Collections.Generic;
using System.Threading.Tasks;
using BTNGNApp.Models;

namespace BTNGNApp.Managers
{
    public interface ICoffeeManager
    {
        Task<string> AddCoffee(Coffee coffee);
        Task<string> DeleteCoffee(string partitionKey, string rowKey);
        Task<Coffee> GetCoffee(string rowKey);
        Task<IEnumerable<Coffee>> GetCoffees();
        Task<string> Update(Coffee coffee);
    }
}