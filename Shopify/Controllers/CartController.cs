using ECommerce_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ECommerce_Project.Controllers
{
    public class CartController : Controller
    {
        readonly Products product = new Products();
        readonly UserModel u = new UserModel();
        readonly CartModel cartItem = new CartModel();
        // GET: Cart
        // Sample data (Replace with data from session or database)
        public ActionResult Index()
        {
            if (Session["UserRole"]?.ToString() == "Customer" || Session["UserRole"]?.ToString() == "Admin")
            {
                string id = Session["UserId"].ToString();
                var cartData = cartItem.GetCarts().Where(m => m.UserId == id).ToList();
                return View(cartData);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }

        }
      
        public ActionResult AddToCart(int id)
        {
            if (Session["UserRole"]?.ToString() == "Customer" || Session["UserRole"]?.ToString() == "Admin")
            {
                var PList = product.GetProduct();
                foreach (var item in PList)
                {
                    if(item.ProductId == id)
                    {
                        var cartItems = new CartModel
                        {
                            ProductId = item.ProductId,
                            ProductName = item.ProductName,
                            ImageUrl = item.ProductImage,
                            Price = item.Price,
                            UserId = Session["UserId"].ToString(),
                            Quantity = 1
                        };
                        if (cartItem.AddCarts(cartItems))
                        {
                            ViewBag.AddCartSuccess = "Your Product Added into Cart!";
                            return RedirectToAction("CustomerDashboard","Home");
                        }
                    }
                }              
                return View("#");
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }

        }
        public ActionResult RemoveFromCart(int id)
        {
            if (cartItem.DeleteCart(id))
            {
                ViewBag.Deletecart = "Product deleted from the cart";
                return RedirectToAction("Index");

            }
            else
            {
                return View();
            }
        }

    }
}