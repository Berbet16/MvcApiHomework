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
    public class ShipperController : Controller
    {
        private readonly HttpClient httpClient;
        public ShipperController()
        {
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://localhost:44313/api/");
        }

        public async Task<ActionResult> Index()
        {
            var response = await httpClient.GetAsync("shipper");
            List<Shipper> shipperList = new List<Shipper>();

            if (response.IsSuccessStatusCode)
            {
                shipperList = JsonConvert.DeserializeObject<List<Shipper>>(await response.Content.ReadAsStringAsync());
            }
    
            return View(shipperList);
        }

        public async Task<ActionResult> Delete(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            await httpClient.DeleteAsync("shipper/" + id);

            return RedirectToAction("Index");
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Add(Shipper shipper)
        {
            var jsonString = JsonConvert.SerializeObject(shipper);
            var stringContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

            await httpClient.PostAsync("shipper", stringContent);

            return RedirectToAction("Index");
        }

        public ActionResult Update(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var response = httpClient.GetStringAsync("shipper/" + id).Result;
            Shipper shipper = JsonConvert.DeserializeObject<Shipper>(response);
    
            if (shipper == null)
            {
                return HttpNotFound();
            }
            return View(shipper);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Update(Shipper shipper)
        {
            var jsonString = JsonConvert.SerializeObject(shipper);
            var stringContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

            await httpClient.PutAsync("shipper/" + shipper.Id, stringContent);

            return RedirectToAction("Index");
        }
    }
}
