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
    public class NorthwindApiProductManager
    {
        static HttpClient client;

        public NorthwindApiProductManager()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://northwind.vercel.app/api/");
        }
        public async Task<List<Product>> GetAllAsync()
        {
            var response = await client.GetAsync("products");

            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<List<Product>>(await response.Content.ReadAsStringAsync());
            }
            return null;
        }
        public IEnumerable<Product> GetProducts()
        {
            var response = client.GetStringAsync("products").Result;
            return JsonConvert.DeserializeObject<IEnumerable<Product>>(response);
        }
        public Product GetProduct(int id)
        {
            var response = client.GetStringAsync("products/" + id).Result;
            return JsonConvert.DeserializeObject<Product>(response);
        }

        public async Task<HttpResponseMessage> AddAsyncProduct(Product product)
        {
            var jsonString = JsonConvert.SerializeObject(product);
            var stringContent = new StringContent(jsonString, Encoding.UTF8, "application/json");
           
            return await client.PostAsync("products", stringContent);

        }

        public async Task<HttpResponseMessage> UpdateAsyncProduct(Product product, int id)
        {
            var jsonString = JsonConvert.SerializeObject(product);
            var stringContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

            return await client.PutAsync("products/" + id, stringContent);
        }

        public async Task<HttpResponseMessage> DeleteAsyncProduct(int id)
        {
            return await client.DeleteAsync("products/" + id);
        }

    }
}
