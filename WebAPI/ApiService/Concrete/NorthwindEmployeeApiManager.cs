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
    public class NorthwindEmployeeApiManager
    {
        static HttpClient client;

        public NorthwindEmployeeApiManager()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://northwind.vercel.app/api/");
        }
        public async Task<List<Employee>> GetAllAsync()
        {
            var response = await client.GetAsync("employess");

            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<List<Employee>>(await response.Content.ReadAsStringAsync());
            }
            return null;
        }
        public IEnumerable<Employee> GetEmployees()
        {
            var response = client.GetStringAsync("employess").Result;
            return JsonConvert.DeserializeObject<IEnumerable<Employee>>(response);
        }
        public Employee GetEmployee(int id)
        {
            var response = client.GetStringAsync("employess/" + id).Result;
            return JsonConvert.DeserializeObject<Employee>(response);
        }

        public async Task<HttpResponseMessage> AddAsyncEmployee(Employee employee)
        {
            var jsonString = JsonConvert.SerializeObject(employee);
            var stringContent = new StringContent(jsonString, Encoding.UTF8, "application/json");
           
            return await client.PostAsync("employess", stringContent);

        }

        public async Task<HttpResponseMessage> UpdateAsyncEmployee(Employee employee, int id)
        {
            var jsonString = JsonConvert.SerializeObject(employee);
            var stringContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

            return await client.PutAsync("employess/" + id, stringContent);
        }

        public async Task<HttpResponseMessage> DeleteAsyncEmployee(int id)
        {
            return await client.DeleteAsync("employess/" + id);
        }

    }
}
