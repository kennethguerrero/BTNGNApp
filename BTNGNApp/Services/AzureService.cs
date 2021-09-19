using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using BTNGNApp.Models;
using BTNGNApp.Services;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace BTNGNApp.Services
{
    public class AzureService : IAzureService
    {
        public async Task<IEnumerable<Coffee>> GetCoffees()
        {
            try
            {
                var url = "https://btngnfunctionapp.azurewebsites.net/api/GetCoffees";
                var httpClient = new HttpClient();
                var response = await httpClient.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    return Enumerable.Empty<Coffee>();
                }

                var json = await response.Content.ReadAsStringAsync();
                var list = JsonConvert.DeserializeObject<IEnumerable<Coffee>>(json).OrderByDescending(c => c.Timestamp);
                return list;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return Enumerable.Empty<Coffee>();
            }
        }

        public async Task<Coffee> GetCoffee(string rowKey)
        {
            var list = await GetCoffees();
            var coffee = list.Where(c => c.RowKey.Equals(rowKey));
            var selectedCoffee = coffee.FirstOrDefault();
            return selectedCoffee;
        }

        public async Task<string> AddCoffee(Coffee coffee)
        {
            var url = $"https://btngnfunctionapp.azurewebsites.net/api/Save/{coffee.CoffeeType}/{coffee.Weight}/{coffee.Quantity}/{coffee.MultipliedWeight}/{coffee.IsStock}/{coffee.SoldTo}";
            var httpClient = new HttpClient();
            string message = await httpClient.GetStringAsync(url);
            return message;
        }

        public async Task<string> DeleteCoffee(string partitionKey, string rowKey)
        {
            var url = $"https://btngnfunctionapp.azurewebsites.net/api/Delete/{partitionKey}/{rowKey}";
            var httpClient = new HttpClient();
            string message = await httpClient.GetStringAsync(url);
            return message;
        }

        public async Task<string> Update(Coffee coffee)
        {
            var url = $"https://btngnfunctionapp.azurewebsites.net/api/Update/{coffee.PartitionKey}/{coffee.RowKey}/{coffee.Type}/{coffee.Weight}/{coffee.Quantity}/{coffee.MultipliedWeight}/{coffee.IsStock}/{coffee.SoldTo}";
            var httpClient = new HttpClient();
            var message = await httpClient.GetStringAsync(url);
            return message;
        }
    }
}
