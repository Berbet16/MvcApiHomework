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
    public class CustomerController : ApiController
    {
        private NorthwindCutomerApiManager northwindApiManager;
        private ILogger logService;
        public CustomerController(ILogger logger)
        {
            northwindApiManager = new NorthwindCutomerApiManager();
            logService = logger;
        }
        // GET: api/Category
        public IEnumerable<Customer> Get()
        {
            Log log = new Log
            {
                Method = "GET",
                Path = "/cutomer/GetAll",
                Query = "",
                CreatedTime = DateTime.Now,
            };
            logService.Write(log);
            var customers = northwindApiManager.GetCustomers();

            return customers;
        }
        public Customer Get(string id)
        {
            Log log = new Log
            {
                Method = "GET",
                Path = "/customer/Get/" + id,
                Query = "",
                CreatedTime = DateTime.Now,
            };
            logService.Write(log);

            Customer customer = northwindApiManager.GetCustomer(id);

            return customer;
        }

        [HttpPost]
        // POST: api/Category
        public async Task<IHttpActionResult> PostAsync([FromBody]Customer customer)
        {
            Log log = new Log
            {
                Method = "POST",
                Path = "/customer/Add/",
                Query = "",
                CreatedTime = DateTime.Now,
            };
            logService.Write(log);
            await northwindApiManager.AddAsyncCustomer(customer);
            return Ok(customer);
        }

        // PUT: api/Category/5
        public async Task<IHttpActionResult> PutAsync(string id, [FromBody]Customer customer)
        {
            Log log = new Log
            {
                Method = "PUT",
                Path = "/customer/Update/" + id,
                Query = "",
                CreatedTime = DateTime.Now,
            };
            logService.Write(log);

            await northwindApiManager.UpdateAsyncCustomer(customer, id);
            return Ok();
        }

        // DELETE: api/Category/5
        public async Task<IHttpActionResult> DeleteAsync(string id)
        {
            Log log = new Log
            {
                Method = "DELETE",
                Path = "/customer/Delete/" +id,
                Query = "",
                CreatedTime = DateTime.Now,
            };
            logService.Write(log);
            await northwindApiManager.DeleteAsyncCustomer(id);
            return Ok();
        }
    }
}
