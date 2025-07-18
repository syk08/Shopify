using ECommerce_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ECommerce_Project.Controllers
{
    public class ElectronicsController : Controller
    {
        ElectronicsModel electronicsproduct = new ElectronicsModel();
        // GET: Electronics Speakers
        public ActionResult Headphones()
        {
            if (Session["UserRole"]?.ToString() == "Customer")
            {
                var data = electronicsproduct.GetProduct().Where(m =>m.Category=="Headphones").ToList();
                return View(data);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        // GET: Electronics TV
        public ActionResult Tv()
        {
            if (Session["UserRole"]?.ToString() == "Customer")
            {
                var data = electronicsproduct.GetProduct().Where(m => m.Category == "Tv").ToList();
                return View(data);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        // GET: Electronics Watch
        public ActionResult Watch()
        {
            if (Session["UserRole"]?.ToString() == "Customer")
            {
                var data = electronicsproduct.GetProduct().Where(m => m.Category == "Watch").ToList();
                return View(data);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }
    }
}