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
    public class ProductController : ApiController
    {
        private NorthwindApiProductManager northwindApiManager;
        private ILogger logService;
        public ProductController(ILogger logger)
        {
            northwindApiManager = new NorthwindApiProductManager();
            logService = logger;
        }
        // GET: api/Category
        public IEnumerable<Product> Get()
        {
            Log log = new Log
            {
                Method = "GET",
                Path = "/product/GetAll",
                Query = "",
                CreatedTime = DateTime.Now,
            };
            logService.Write(log);
            var products = northwindApiManager.GetProducts();

            return products;
        }

        public Product Get(int id)
        {
            Log log = new Log
            {
                Method = "GET",
                Path = "/product/" + id,
                Query = "",
                CreatedTime = DateTime.Now,
            };
            logService.Write(log);
            Product product = northwindApiManager.GetProduct(id);

            return product;
        }

        [HttpPost]
        // POST: api/Category
        public async Task<IHttpActionResult> PostAsync([FromBody]Product product)
        {
            Log log = new Log
            {
                Method = "POST",
                Path = "/product/Add",
                Query = "",
                CreatedTime = DateTime.Now,
            };
            logService.Write(log);
            await northwindApiManager.AddAsyncProduct(product);
            return Ok(product);
        }

        // PUT: api/Category/5
        public async Task<IHttpActionResult> PutAsync(int id, [FromBody] Product product)
        {
            Log log = new Log
            {
                Method = "PUT",
                Path = "/product/Update/" + id,
                Query = "",
                CreatedTime = DateTime.Now,
            };
            logService.Write(log);
            await northwindApiManager.UpdateAsyncProduct(product, id);
            return Ok();
        }

        // DELETE: api/Category/5
        public async Task<IHttpActionResult> DeleteAsync(int id)
        {
            Log log = new Log
            {
                Method = "DELETE",
                Path = "/product/Delete/"+ id,
                Query = "",
                CreatedTime = DateTime.Now,
            };
            logService.Write(log);
            await northwindApiManager.DeleteAsyncProduct(id);
            return Ok();
        }
    }
}
