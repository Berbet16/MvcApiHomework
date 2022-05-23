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
    public class SupplierController : Controller
    {
        private readonly HttpClient httpClient;
        public SupplierController()
        {
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://localhost:44313/api/");
        }

        public async Task<ActionResult> Index()
        {
            var response = await httpClient.GetAsync("supplier");
            List<Supplier> supplierList = new List<Supplier>();

            if (response.IsSuccessStatusCode)
            {
                supplierList = JsonConvert.DeserializeObject<List<Supplier>>(await response.Content.ReadAsStringAsync());
            }
    
            return View(supplierList);
        }

        public async Task<ActionResult> Delete(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            await httpClient.DeleteAsync("supplier/" + id);

            return RedirectToAction("Index");
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Add(Supplier supplier)
        {
            var jsonString = JsonConvert.SerializeObject(supplier);
            var stringContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

            await httpClient.PostAsync("supplier", stringContent);

            return RedirectToAction("Index");
        }

        public ActionResult Update(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var response = httpClient.GetStringAsync("supplier/" + id).Result;
            Supplier supplier = JsonConvert.DeserializeObject<Supplier>(response);
    
            if (supplier == null)
            {
                return HttpNotFound();
            }
            return View(supplier);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Update(Supplier supplier)
        {
            var jsonString = JsonConvert.SerializeObject(supplier);
            var stringContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

            await httpClient.PutAsync("supplier/" + supplier.Id, stringContent);

            return RedirectToAction("Index");
        }
    }
}
