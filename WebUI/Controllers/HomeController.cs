using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebUI.Controllers
{
    public class HomeController : Controller
    {
        private ILogger logger;

        public HomeController(ILogger logger)
        {
            this.logger = logger;
        }

        private void Log()
        {
            Log log = new Log();
            log.Method = Request.HttpMethod;
            log.Query = Request.QueryString.ToString();
            log.Path = Request.Path;
            log.CreatedTime = DateTime.Now;
            logger.Write(log);
        }
        public ActionResult Index(string query)
        {
            Log();

            IEnumerable<Log> list;

            if (query == null)
            {
                list = logger.ReadAll();
            }
            else
            {
                list = logger.Search(query);
            }

            
            return View(list);
        }

        public ActionResult About()
        {
            Log();

            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            Log();

            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}