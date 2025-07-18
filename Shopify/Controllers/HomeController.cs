using ECommerce_Project.Models;
using System.Web.Mvc;

namespace ECommerce_Project.Controllers
{
    public class HomeController : Controller
    {
        Products p = new Products();
        // Admin Dashboard
        public ActionResult AdminDashboard()
        {
            if (Session["UserRole"]?.ToString() == "Admin")
            {
                return View();
            }
            return RedirectToAction("Login", "Account");
        }

        // Customer Dashboard
        public ActionResult CustomerDashboard()
        {
            if (Session["UserRole"]?.ToString() == "Customer")
            {
                var data = p.GetProduct();
                return View(data);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
           
        }
    }
}
