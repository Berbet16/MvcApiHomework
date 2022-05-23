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
    public class SupplierController : ApiController
    {
        private NorthwindSupplierApiManager northwindApiManager;
        private ILogger logService;
        public SupplierController(ILogger logger)
        {
            northwindApiManager = new NorthwindSupplierApiManager();
            logService = logger;
        }
        // GET: api/Category
        public IEnumerable<Supplier> Get()
        {
            Log log = new Log
            {
                Method = "GET",
                Path = "/supplier/GetAll",
                Query = "",
                CreatedTime = DateTime.Now,
            };
            logService.Write(log);
            var suppliers = northwindApiManager.GetSuppliers(); 

            return suppliers;
        }

        //public async Task<IHttpActionResult> Get()
        //{
        //    var categories = await northwindApiManager.GetAllAsync();

        //    return Ok(categories);
        //}

        // GET: api/Category/5
        public Supplier Get(int id)
        {
            Log log = new Log
            {
                Method = "GET",
                Path = "/supplier/Get/" + id,
                Query = "",
                CreatedTime = DateTime.Now,
            };
            logService.Write(log);

            Supplier supplier = northwindApiManager.GetSupplier(id);

            return supplier;
        }

        [HttpPost]
        // POST: api/Category
        public async Task<IHttpActionResult> PostAsync([FromBody]Supplier supplier)
        {
            Log log = new Log
            {
                Method = "POST",
                Path = "/supplier/Add/",
                Query = "",
                CreatedTime = DateTime.Now,
            };
            logService.Write(log);
            await northwindApiManager.AddAsyncSupplier(supplier);
            return Ok(supplier);
        }

        // PUT: api/Category/5
        public async Task<IHttpActionResult> PutAsync(int id, [FromBody]Supplier supplier)
        {
            Log log = new Log
            {
                Method = "PUT",
                Path = "/supplier/Update/" + id,
                Query = "",
                CreatedTime = DateTime.Now,
            };
            logService.Write(log);

            await northwindApiManager.UpdateAsyncSupplier(supplier, id);
            return Ok();
        }

        // DELETE: api/Category/5
        public async Task<IHttpActionResult> DeleteAsync(int id)
        {
            Log log = new Log
            {
                Method = "DELETE",
                Path = "/supplier/Delete/" + id,
                Query = "",
                CreatedTime = DateTime.Now,
            };
            logService.Write(log);
            await northwindApiManager.DeleteAsyncSupplier(id);
            return Ok();
        }
    }
}
