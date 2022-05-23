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
    public class CategoryController : ApiController
    {
        private NorthwindApiManager northwindApiManager;
        private ILogger logService;
        public CategoryController(ILogger logger)
        {
            northwindApiManager = new NorthwindApiManager();
            logService = logger;
        }
        // GET: api/Category
        public IEnumerable<Category> Get()
        {
            Log log = new Log
            {
                Method = "GET",
                Path = "/category/GetAll",
                Query = "",
                CreatedTime = DateTime.Now,
            };
            logService.Write(log);
            var categories = northwindApiManager.GetCategories();

            return categories;
        }

        //public async Task<IHttpActionResult> Get()
        //{
        //    var categories = await northwindApiManager.GetAllAsync();

        //    return Ok(categories);
        //}

        // GET: api/Category/5
        public Category Get(int id)
        {
            Log log = new Log
            {
                Method = "GET",
                Path = "/category/Get/" + id,
                Query = "",
                CreatedTime = DateTime.Now,
            };
            logService.Write(log);

            Category category = northwindApiManager.GetCategory(id);

            return category;
        }

        [HttpPost]
        // POST: api/Category
        public async Task<IHttpActionResult> PostAsync([FromBody]Category category)
        {
            Log log = new Log
            {
                Method = "POST",
                Path = "/category/Add/",
                Query = "",
                CreatedTime = DateTime.Now,
            };
            logService.Write(log);
            await northwindApiManager.AddAsyncCategory(category);
            return Ok(category);
        }

        // PUT: api/Category/5
        public async Task<IHttpActionResult> PutAsync(int id, [FromBody]Category category)
        {
            Log log = new Log
            {
                Method = "PUT",
                Path = "/category/Update/" + id,
                Query = "",
                CreatedTime = DateTime.Now,
            };
            logService.Write(log);

            await northwindApiManager.UpdateAsyncCategory(category, id);
            return Ok();
        }

        // DELETE: api/Category/5
        public async Task<IHttpActionResult> DeleteAsync(int id)
        {
            Log log = new Log
            {
                Method = "DELETE",
                Path = "/category/Delete/" +id,
                Query = "",
                CreatedTime = DateTime.Now,
            };
            logService.Write(log);
            await northwindApiManager.DeleteAsyncCategory(id);
            return Ok();
        }
    }
}
