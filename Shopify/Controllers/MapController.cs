using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ECommerce_Project.Controllers
{
    public class MapController : Controller
    {
        // GET: Map
        public ActionResult Index()
        {
            ViewBag.GoMapsApiKey = ConfigurationManager.AppSettings["GoMapsApiKey"];
            return View();
        }
    }
}