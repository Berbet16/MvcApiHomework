using Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.ApiService.Concrete
{
    public class NorthwindCutomerApiManager
    {
        static HttpClient client;

        public NorthwindCutomerApiManager()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://northwind.vercel.app/api/");
        }
        public async Task<List<Customer>> GetAllAsync()
        {
            var response = await client.GetAsync("customers");

            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<List<Customer>>(await response.Content.ReadAsStringAsync());
            }
            return null;
        }
        public IEnumerable<Customer> GetCustomers()
        {
            var response = client.GetStringAsync("customers").Result;
            return JsonConvert.DeserializeObject<IEnumerable<Customer>>(response);
        }
        public Customer GetCustomer(string id)
        {
            var response = client.GetStringAsync("customers/" + id).Result;
            return JsonConvert.DeserializeObject<Customer>(response);
        }

        public async Task<HttpResponseMessage> AddAsyncCustomer(Customer customer)
        {
            var jsonString = JsonConvert.SerializeObject(customer);
            var stringContent = new StringContent(jsonString, Encoding.UTF8, "application/json");
           
            return await client.PostAsync("customers", stringContent);

        }

        public async Task<HttpResponseMessage> UpdateAsyncCustomer(Customer customer, string id)
        {
            var jsonString = JsonConvert.SerializeObject(customer);
            var stringContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

            return await client.PutAsync("customers/" + id, stringContent);
        }

        public async Task<HttpResponseMessage> DeleteAsyncCustomer(string id)
        {
            return await client.DeleteAsync("customers/" + id);
        }

    }
}
