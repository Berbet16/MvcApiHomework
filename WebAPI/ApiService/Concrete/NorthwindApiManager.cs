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
    public class NorthwindApiManager
    {
        static HttpClient client;

        public NorthwindApiManager()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://northwind.vercel.app/api/");
        }
        public async Task<List<Category>> GetAllAsync()
        {
            var response = await client.GetAsync("categories");

            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<List<Category>>(await response.Content.ReadAsStringAsync());
            }
            return null;
        }
        public IEnumerable<Category> GetCategories()
        {
            var response = client.GetStringAsync("categories").Result;
            return JsonConvert.DeserializeObject<IEnumerable<Category>>(response);
        }
        public Category GetCategory(int id)
        {
            var response = client.GetStringAsync("categories/" + id).Result;
            return JsonConvert.DeserializeObject<Category>(response);
        }

        public async Task<HttpResponseMessage> AddAsyncCategory(Category category)
        {
            var jsonString = JsonConvert.SerializeObject(category);
            var stringContent = new StringContent(jsonString, Encoding.UTF8, "application/json");
           
            return await client.PostAsync("categories", stringContent);

        }

        public async Task<HttpResponseMessage> UpdateAsyncCategory(Category category, int id)
        {
            var jsonString = JsonConvert.SerializeObject(category);
            var stringContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

            return await client.PutAsync("categories/" + id, stringContent);
        }

        public async Task<HttpResponseMessage> DeleteAsyncCategory(int id)
        {
            return await client.DeleteAsync("categories/" + id);
        }

    }
}
