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
    public class OrderController : ApiController
    {
        private NorthwindApiOrderManager northwindApiManager;
        private ILogger logService;
        public OrderController(ILogger logger)
        {
            northwindApiManager = new NorthwindApiOrderManager();
            logService = logger;
        }
        // GET: api/Category
        public IEnumerable<Order> Get()
        {
            Log log = new Log
            {
                Method = "GET",
                Path = "/order/GetAll",
                Query = "",
                CreatedTime = DateTime.Now,
            };
            logService.Write(log);
            var orders = northwindApiManager.GetOrders();

            return orders;
        }

        public Order Get(int id)
        {
            Log log = new Log
            {
                Method = "GET",
                Path = "/order/" + id,
                Query = "",
                CreatedTime = DateTime.Now,
            };
            logService.Write(log);
            Order order = northwindApiManager.GetOrder(id);

            return order;
        }

        [HttpPost]
        // POST: api/Category
        public async Task<IHttpActionResult> PostAsync([FromBody]Order order)
        {
            Log log = new Log
            {
                Method = "POST",
                Path = "/order/Add",
                Query = "",
                CreatedTime = DateTime.Now,
            };
            logService.Write(log);
            await northwindApiManager.AddAsyncOrder(order);
            return Ok(order);
        }

        // PUT: api/Category/5
        public async Task<IHttpActionResult> PutAsync(int id, [FromBody] Order order)
        {
            Log log = new Log
            {
                Method = "PUT",
                Path = "/order/Update/" + id,
                Query = "",
                CreatedTime = DateTime.Now,
            };
            logService.Write(log);
            await northwindApiManager.UpdateAsyncOrder(order, id);
            return Ok();
        }

        // DELETE: api/Category/5
        public async Task<IHttpActionResult> DeleteAsync(int id)
        {
            Log log = new Log
            {
                Method = "DELETE",
                Path = "/order/Delete/"+ id,
                Query = "",
                CreatedTime = DateTime.Now,
            };
            logService.Write(log);
            await northwindApiManager.DeleteAsyncOrder(id);
            return Ok();
        }
    }
}
