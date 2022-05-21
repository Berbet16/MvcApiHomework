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
    public class NorthwindShipperApiManager
    {
        static HttpClient client;

        public NorthwindShipperApiManager()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://northwind.vercel.app/api/");
        }
        public async Task<List<Shipper>> GetAllAsync()
        {
            var response = await client.GetAsync("shippers");

            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<List<Shipper>>(await response.Content.ReadAsStringAsync());
            }
            return null;
        }
        public IEnumerable<Shipper> GetShippers()
        {
            var response = client.GetStringAsync("shippers").Result;
            return JsonConvert.DeserializeObject<IEnumerable<Shipper>>(response);
        }
        public Shipper GetShipper(int id)
        {
            var response = client.GetStringAsync("shippers/" + id).Result;
            return JsonConvert.DeserializeObject<Shipper>(response);
        }

        public async Task<HttpResponseMessage> AddAsyncShipper(Shipper shipper)
        {
            var jsonString = JsonConvert.SerializeObject(shipper);
            var stringContent = new StringContent(jsonString, Encoding.UTF8, "application/json");
           
            return await client.PostAsync("shippers", stringContent);

        }

        public async Task<HttpResponseMessage> UpdateAsyncShipper(Shipper shipper, int id)
        {
            var jsonString = JsonConvert.SerializeObject(shipper);
            var stringContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

            return await client.PutAsync("shippers/" + id, stringContent);
        }

        public async Task<HttpResponseMessage> DeleteAsyncShipper(int id)
        {
            return await client.DeleteAsync("shippers/" + id);
        }

    }
}
