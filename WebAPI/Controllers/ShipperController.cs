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
    public class ShipperController : ApiController
    {
        private NorthwindShipperApiManager northwindApiManager;
        private ILogger logService;
        public ShipperController(ILogger logger)
        {
            northwindApiManager = new NorthwindShipperApiManager();
            logService = logger;
        }
        // GET: api/Category
        public IEnumerable<Shipper> Get()
        {
            Log log = new Log
            {
                Method = "GET",
                Path = "/shipper/GetAll",
                Query = "",
                CreatedTime = DateTime.Now,
            };
            logService.Write(log);
            var shippers = northwindApiManager.GetShippers();

            return shippers;
        }

        public Shipper Get(int id)
        {
            Log log = new Log
            {
                Method = "GET",
                Path = "/shipper/Get/" + id,
                Query = "",
                CreatedTime = DateTime.Now,
            };
            logService.Write(log);

            Shipper shipper = northwindApiManager.GetShipper(id);

            return shipper;
        }

        [HttpPost]
        // POST: api/Category
        public async Task<IHttpActionResult> PostAsync([FromBody]Shipper shipper)
        {
            Log log = new Log
            {
                Method = "POST",
                Path = "/shipper/Add/",
                Query = "",
                CreatedTime = DateTime.Now,
            };
            logService.Write(log);
            await northwindApiManager.AddAsyncShipper(shipper);
            return Ok(shipper);
        }

        // PUT: api/Category/5
        public async Task<IHttpActionResult> PutAsync(int id, [FromBody] Shipper shipper)
        {
            Log log = new Log
            {
                Method = "PUT",
                Path = "/shipper/Update/" + id,
                Query = "",
                CreatedTime = DateTime.Now,
            };
            logService.Write(log);

            await northwindApiManager.UpdateAsyncShipper(shipper, id);
            return Ok();
        }

        // DELETE: api/Category/5
        public async Task<IHttpActionResult> DeleteAsync(int id)
        {
            Log log = new Log
            {
                Method = "DELETE",
                Path = "/shipper/Delete/" +id,
                Query = "",
                CreatedTime = DateTime.Now,
            };
            logService.Write(log);
            await northwindApiManager.DeleteAsyncShipper(id);
            return Ok();
        }
    }
}
