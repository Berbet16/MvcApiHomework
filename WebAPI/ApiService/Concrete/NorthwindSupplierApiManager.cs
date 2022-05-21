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
    public class NorthwindSupplierApiManager
    {
        static HttpClient client;

        public NorthwindSupplierApiManager()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://northwind.vercel.app/api/");
        }
        public async Task<List<Supplier>> GetAllAsync()
        {
            var response = await client.GetAsync("suppliers");

            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<List<Supplier>>(await response.Content.ReadAsStringAsync());
            }
            return null;
        }
        public IEnumerable<Supplier> GetSuppliers()
        {
            var response = client.GetStringAsync("suppliers").Result;
            return JsonConvert.DeserializeObject<IEnumerable<Supplier>>(response);
        }
        public Supplier GetSupplier(int id)
        {
            var response = client.GetStringAsync("suppliers/" + id).Result;
            return JsonConvert.DeserializeObject<Supplier>(response);
        }

        public async Task<HttpResponseMessage> AddAsyncSupplier(Supplier supplier)
        {
            var jsonString = JsonConvert.SerializeObject(supplier);
            var stringContent = new StringContent(jsonString, Encoding.UTF8, "application/json");
           
            return await client.PostAsync("suppliers", stringContent);

        }

        public async Task<HttpResponseMessage> UpdateAsyncSupplier(Supplier supplier, int id)
        {
            var jsonString = JsonConvert.SerializeObject(supplier);
            var stringContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

            return await client.PutAsync("suppliers/" + id, stringContent);
        }

        public async Task<HttpResponseMessage> DeleteAsyncSupplier(int id)
        {
            return await client.DeleteAsync("suppliers/" + id);
        }

    }
}
