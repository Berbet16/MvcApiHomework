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
    public class ProductController : Controller
    {
        private readonly HttpClient httpClient;
        public ProductController()
        {
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://localhost:44313/api/");
        }

        public async Task<ActionResult> Index()
        {
            var response = await httpClient.GetAsync("product");
            List<Product> productList = new List<Product>();

            if (response.IsSuccessStatusCode)
            {
                productList = JsonConvert.DeserializeObject<List<Product>>(await response.Content.ReadAsStringAsync());
            }
    
            return View(productList);
        }

        public async Task<ActionResult> Delete(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            await httpClient.DeleteAsync("product/" + id);

            return RedirectToAction("Index");
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Add(Product product)
        {
            var jsonString = JsonConvert.SerializeObject(product);
            var stringContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

            await httpClient.PostAsync("product", stringContent);

            return RedirectToAction("Index");
        }

        public ActionResult Update(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var response = httpClient.GetStringAsync("product/" + id).Result;
            Product product = JsonConvert.DeserializeObject<Product>(response);
    
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Update(Product product)
        {
            var jsonString = JsonConvert.SerializeObject(product);
            var stringContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

            await httpClient.PutAsync("product/" + product.Id, stringContent);

            return RedirectToAction("Index");
        }
    }
}
