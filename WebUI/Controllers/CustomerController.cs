using Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Homework.WebUI.Controllers
{
    public class CustomerController : Controller
    {
        private readonly HttpClient httpClient;
        public CustomerController()
        {
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://localhost:44313/api/");
        }

        public async Task<ActionResult> Index()
        {
            var response = await httpClient.GetAsync("customer");
            List<Customer> customerList = new List<Customer>();

            if (response.IsSuccessStatusCode)
            {
                customerList = JsonConvert.DeserializeObject<List<Customer>>(await response.Content.ReadAsStringAsync());
            }
    
            return View(customerList);
        }

        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            await httpClient.DeleteAsync("customer/" + id);

            return RedirectToAction("Index");
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Add(Customer customer)
        {
            var jsonString = JsonConvert.SerializeObject(customer);
            var stringContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

            await httpClient.PostAsync("customer", stringContent);

            return RedirectToAction("Index");
        }

        public ActionResult Update(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var response = httpClient.GetStringAsync("customer/" + id).Result;
            Customer customer = JsonConvert.DeserializeObject<Customer>(response);
    
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Update(Customer customer)
        {
            var jsonString = JsonConvert.SerializeObject(customer);
            var stringContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

            await httpClient.PutAsync("customer/" + customer.Id, stringContent);

            return RedirectToAction("Index");
        }
    }
}
