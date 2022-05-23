using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPI.ApiService.Concrete;
using System.Web.Http.Results;
using System.Threading.Tasks;


namespace WebAPI.Controllers
{
    public class EmployeeController : ApiController
    {
        private NorthwindEmployeeApiManager northwindApiManager;
        private ILogger logService;
        public EmployeeController(ILogger logger)
        {
            northwindApiManager = new NorthwindEmployeeApiManager();
            logService = logger;
        }
        // GET: api/Category
        public IEnumerable<Employee> Get()
        {
            Log log = new Log
            {
                Method = "GET",
                Path = "/employee/GetAll",
                Query = "",
                CreatedTime = DateTime.Now,
            };
            logService.Write(log);
            var employees = northwindApiManager.GetEmployees();

            return employees;
        }

        // GET: api/Category/5
        public Employee Get(int id)
        {
            Log log = new Log
            {
                Method = "GET",
                Path = "/employee/Get/" + id,
                Query = "",
                CreatedTime = DateTime.Now,
            };
            logService.Write(log);

            Employee employee = northwindApiManager.GetEmployee(id);

            return employee;
        }

        [HttpPost]
        // POST: api/Category
        public async Task<IHttpActionResult> PostAsync([FromBody]Employee employee)
        {
            Log log = new Log
            {
                Method = "POST",
                Path = "/employee/Add/",
                Query = "",
                CreatedTime = DateTime.Now,
            };
            logService.Write(log);
            await northwindApiManager.AddAsyncEmployee(employee);
            return Ok(employee);
        }

        // PUT: api/Category/5
        public async Task<IHttpActionResult> PutAsync(int id, [FromBody]Employee employee)
        {
            Log log = new Log
            {
                Method = "PUT",
                Path = "/employee/Update/" + id,
                Query = "",
                CreatedTime = DateTime.Now,
            };
            logService.Write(log);

            await northwindApiManager.UpdateAsyncEmployee(employee, id);
            return Ok();
        }

        // DELETE: api/Category/5
        public async Task<IHttpActionResult> DeleteAsync(int id)
        {
            Log log = new Log
            {
                Method = "DELETE",
                Path = "/employee/Delete/" + id,
                Query = "",
                CreatedTime = DateTime.Now,
            };
            logService.Write(log);
            await northwindApiManager.DeleteAsyncEmployee(id);
            return Ok();
        }
    }
}
