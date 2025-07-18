using ECommerce_Project.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace ECommerce_Project.Controllers
{
    public class AccountController : Controller
    {
        public new ActionResult Profile()
        {
            // Assuming you have a logged-in user, retrieve user data (dummy example)
            if (Session["UserRole"].ToString() == "Customer" || Session["UserRole"].ToString() == "Admin")
            {
                var data = new ProfileModel()
                {

                    FullName = Session["UserName"].ToString(),
                    Email = Session["UserEmail"].ToString(),
                    ProfilePicture = "/uploads/default-profile.jpg" // Default image
                };
                return View(data);
            }
            else
            {
                return HttpNotFound();
            }
        }

        // POST: Account/Profile (Handle Profile Picture Upload)
        [HttpPost]
        public new ActionResult Profile(HttpPostedFileBase profileImage)
        {
            var model = new ProfileModel
            {
                FullName = Session["UserName"].ToString(), // You should load this from the database
                Email = Session["UserEmail"].ToString(),
                ProfilePicture = Session["ProfilePicture"] != null ? Session["ProfilePicture"].ToString() : "/uploads/default-profile.jpg"
            };
            if (profileImage != null && profileImage.ContentLength > 0)
            {
                string fileName = Path.GetFileName(profileImage.FileName);
                string path = Path.Combine(Server.MapPath("~/uploads/"), fileName);

                // Ensure directory exists
                if (!Directory.Exists(Server.MapPath("~/uploads/")))
                {
                    Directory.CreateDirectory(Server.MapPath("~/uploads/"));
                }

                profileImage.SaveAs(path);

                // Store image path in Session (or save in DB for persistent storage)
                Session["ProfilePicture"] = "/uploads/" + fileName;
                ViewBag.ProfilePicture = Session["ProfilePicture"];

                ViewBag.Message = "Profile picture updated successfully!";
            }

            return View(model);
        }

        // GET: Login Page
        public ActionResult Login()
        {
            return View();
        }

        // POST: Login User
        [HttpPost]
        public ActionResult Login(UserModel user)
        {
            if (ModelState.IsValid)
            {
                if (user.ValidateUser())
                {
                    Session["UserEmail"] = user.Email.ToString();
                    var data = user.GetUser();
                    foreach (var item in data)
                    {
                        if ( user.Email == item.Email)
                        {
                            Session["UserName"] = item.FullName.ToString();
                            Session["UserId"] = item.UserID.ToString();
                            Session["UserRole"] = item.Role;
                        }
                    }

                    TempData["LoginSuccessfullMessage"] = "<script>alert('Loged In Successfully')</script>";
                    if (user.Role == "Admin")
                    {
                        return RedirectToAction("AdminDashboard", "Home");
                    }
                    else
                    {
                        return RedirectToAction("CustomerDashboard", "Home");
                    }
                }
                else
                {
                    ViewBag.ErrorMessage = "<script>alert('Invalid Email or Password!')</script>";
                    return View();
                }
            }
            else
            {
                return View();
            }
        }

        // GET: Signup Page
        public ActionResult Signup()
        {
            return View();
        }

        // POST: Register User
        [HttpPost]
        public ActionResult Signup(UserModel user)
        {
            if (ModelState.IsValid)
            {
                if (user.RegisterUser())
                {
                    TempData["RegisterMessage"] = "<script>alert('Registered Successfully')</script>";
                    Session["UserId"] = user.UserID.ToString();
                    Session["UserEmail"] = user.Email;
                    Session["UserName"] = user.FullName.ToString();
                    Session["UserRole"] = user.Role;

                    if (user.Role == "Admin")
                    {
                        return RedirectToAction("AdminDashboard", "Home");
                    }
                    else
                    {
                        return RedirectToAction("CustomerDashboard", "Home");
                    }
                }
                else
                {
                    ViewBag.RegisterMessage = "<script>alert('Registration Failed')</script>";
                    return View();
                }
            }
            return View();
        }

        // Logout
        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Login");
        }
    }
}
