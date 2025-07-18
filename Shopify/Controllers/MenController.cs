using ECommerce_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ECommerce_Project.Controllers
{
    public class MenController : Controller
    {
        //private readonly MenProduct product;
        //MenController()
        //{
        //    product = new MenProduct();
        //}
        MenProduct product = new MenProduct();
        // GET: Men Pants
        public ActionResult Pants()
        {
            if (Session["UserRole"]?.ToString() == "Customer")
            {
                var men = product.GetProduct().Where(m => m.Category =="Pants").ToList();
                return View(men);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        // GET: Men Shirt
        public ActionResult Shirt()
        {
            if (Session["UserRole"]?.ToString() == "Customer")
            {
                var men = product.GetProduct().Where(m => m.Category == "Shirts").ToList();
                return View(men);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        // GET: Men Shoes
        public ActionResult Shoes()
        {
            if (Session["UserRole"]?.ToString() == "Customer")
            {
                var men = product.GetProduct().Where(m => m.Category == "Shoes").ToList();
                return View(men);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }
    }
}