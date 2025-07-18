using ECommerce_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ECommerce_Project.Controllers
{
    public class WomenController : Controller
    {
        WomenModel product = new WomenModel();

        // GET: Women Pants
        public ActionResult Handbags()
        {
            if (Session["UserRole"]?.ToString() == "Customer")
            {
                var women = product.GetProduct().Where(m => m.Category == "Handbags").ToList();
                return View(women);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        // GET: Women Top
        public ActionResult Dresses()
        {
            if (Session["UserRole"]?.ToString() == "Customer")
            {
                var women = product.GetProduct().Where(m => m.Category == "Dresses").ToList();
                return View(women);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        // GET: Women Shoes
        public ActionResult Shoes()
        {
            if (Session["UserRole"]?.ToString() == "Customer")
            {
                var women = product.GetProduct().Where(m => m.Category == "Shoes").ToList();
                return View(women);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }
    }
}