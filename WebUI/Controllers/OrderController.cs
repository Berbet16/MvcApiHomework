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
namespace WebUI.Controllers
{
    public class OrderController : Controller
    {
        private readonly HttpClient httpClient;
        public OrderController()
        {
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://localhost:44313/api/");
        }

        public async Task<ActionResult> Index()
        {
            var response = await httpClient.GetAsync("order");
            List<Order> orderList = new List<Order>();

            if (response.IsSuccessStatusCode)
            {
                orderList = JsonConvert.DeserializeObject<List<Order>>(await response.Content.ReadAsStringAsync());
            }

            return View(orderList);
        }

        public async Task<ActionResult> Delete(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            await httpClient.DeleteAsync("order/" + id);

            return RedirectToAction("Index");
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Add(Order order)
        {
            var jsonString = JsonConvert.SerializeObject(order);
            var stringContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

            await httpClient.PostAsync("order", stringContent);

            return RedirectToAction("Index");
        }

        public ActionResult Update(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var response = httpClient.GetStringAsync("order/" + id).Result;
            Order order = JsonConvert.DeserializeObject<Order>(response);

            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Update(Order order)
        {
            var jsonString = JsonConvert.SerializeObject(order);
            var stringContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

            await httpClient.PutAsync("order/" + order.Id, stringContent);

            return RedirectToAction("Index");
        }
    }
}
