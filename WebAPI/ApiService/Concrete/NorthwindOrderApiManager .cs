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
    public class NorthwindApiOrderManager
    {
        static HttpClient client;

        public NorthwindApiOrderManager()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://northwind.vercel.app/api/");
        }
        public async Task<List<Order>> GetAllAsync()
        {
            var response = await client.GetAsync("orders");

            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<List<Order>>(await response.Content.ReadAsStringAsync());
            }
            return null;
        }
        public IEnumerable<Order> GetOrders()
        {
            var response = client.GetStringAsync("orders").Result;
            return JsonConvert.DeserializeObject<IEnumerable<Order>>(response);
        }
        public Order GetOrder(int id)
        {
            var response = client.GetStringAsync("orders/" + id).Result;
            return JsonConvert.DeserializeObject<Order>(response);
        }

        public async Task<HttpResponseMessage> AddAsyncOrder(Order order)
        {
            var jsonString = JsonConvert.SerializeObject(order);
            var stringContent = new StringContent(jsonString, Encoding.UTF8, "application/json");
           
            return await client.PostAsync("orders", stringContent);

        }

        public async Task<HttpResponseMessage> UpdateAsyncOrder(Order order, int id)
        {
            var jsonString = JsonConvert.SerializeObject(order);
            var stringContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

            return await client.PutAsync("orders/" + id, stringContent);
        }

        public async Task<HttpResponseMessage> DeleteAsyncOrder(int id)
        {
            return await client.DeleteAsync("orders/" + id);
        }

    }
}
