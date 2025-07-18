using ECommerce_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ECommerce_Project.Controllers
{
    public class OrderController : Controller
    {
        private readonly OrderModel orderModel;
        private readonly Products products;
        private readonly UserModel users;
        public OrderController()
        {
            orderModel = new OrderModel();
            products = new Products();
            users = new UserModel();
        }
        // GET: Order details for Only Admin
        public ActionResult Index()
        {
            if (Session["UserRole"].ToString() == "Admin")
            {
                var OrderData = orderModel.GetOrders();
                return View(OrderData);
            }
            else
            {
                return HttpNotFound();
            }
            
        } 
        
        public ActionResult PlaceOrder(int id)
        {
            if (Session["UserRole"].ToString() == "Customer")
            {
                int Id = Convert.ToInt32(Session["UserId"]);
                var productData = products.GetProduct().FirstOrDefault(model => model.ProductId == id);
                var UserData = users.GetUser().FirstOrDefault(model => model.UserID == Id);

                var data = new UserProductModel
                {
                    Products = productData,
                    UserModel = UserData,
                    time = DateTime.Now,
                    quantity = 1
                };
                
                if (orderModel.AddOrder(data))
                {
                    TempData["placeOrderMessage"] = "<script>alert('Congratulation 😀, Your Order Is Successfully Placed')</script>";
                    return RedirectToAction("CustomerDashboard","Home");
                }
                return View();
            }
            else
            {
                return HttpNotFound();
            }
            
        }
    
        // Get : Order details for only Customers 

        public ActionResult CheckOrders()
        {
            if (Session["UserRole"].ToString() == "Customer" || Session["UserRole"].ToString() == "Admin")
            {
                int id = Convert.ToInt32(Session["UserId"]);
                var data = orderModel.GetOrders().Where(m => m.UserId == id).ToList();
                return View(data);
            }
            else
            {
                return HttpNotFound();
            }
           
        }
        
       
    }
}