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
    public class EmployeeController : Controller
    {
        private readonly HttpClient httpClient;
        public EmployeeController()
        {
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://localhost:44313/api/");
        }

        public async Task<ActionResult> Index()
        {
            var response = await httpClient.GetAsync("employee");
            List<Employee> employeeList = new List<Employee>();

            if (response.IsSuccessStatusCode)
            {
                employeeList = JsonConvert.DeserializeObject<List<Employee>>(await response.Content.ReadAsStringAsync());
            }
    
            return View(employeeList);
        }

        public async Task<ActionResult> Delete(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            await httpClient.DeleteAsync("employee/" + id);

            return RedirectToAction("Index");
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Add(Employee employee)
        {
            var jsonString = JsonConvert.SerializeObject(employee);
            var stringContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

            await httpClient.PostAsync("employee", stringContent);

            return RedirectToAction("Index");
        }

        public ActionResult Update(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var response = httpClient.GetStringAsync("employee/" + id).Result;
            Employee employee = JsonConvert.DeserializeObject<Employee>(response);
    
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Update(Employee employee)
        {
            var jsonString = JsonConvert.SerializeObject(employee);
            var stringContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

            await httpClient.PutAsync("employee/" + employee.Id, stringContent);

            return RedirectToAction("Index");
        }
    }
}
