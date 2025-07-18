using ECommerce_Project.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ECommerce_Project.Controllers
{
    public class ProductController : Controller
    {
        private readonly Products Product = new Products();
        private readonly MenProduct menProduct = new MenProduct();
        private readonly WomenModel womenProduct = new WomenModel();
        private readonly ElectronicsModel electronicsProduct = new ElectronicsModel();

        // GET: Product
        public ActionResult Index(string SearchedBy, string search)

        {
            if (SearchedBy == "Category" && search != null)
            {
                var data = Product.GetProduct().Where(m => m.Category.StartsWith(search)).ToList();
                return View(data);
            }
            else if (SearchedBy == "ProductName" && search != null)
            {
                var data = Product.GetProduct().Where(m => m.ProductName.StartsWith(search)).ToList();
                return View(data);
            }
            else
            {
                var data = Product.GetProduct();
                if (Session["UserRole"]?.ToString() == "Admin")
                {
                    return View(data);
                }
                else
                {
                    return RedirectToAction("CustomerDashboard", "Home");
                }
            }


        }

        public ActionResult AddProduct()
        {
            if (Session["UserRole"]?.ToString() == "Admin")
            {
                return View();
            }
            return RedirectToAction("CustomerDashboard", "Home");
        }
        [HttpPost]
        public ActionResult AddProduct(Products product)
        {
            if (product.PostedFile.FileName != null && product.PostedFile.ContentLength > 0)
            {
                string fileName = Path.GetFileNameWithoutExtension(product.PostedFile.FileName);
                string Extension = Path.GetExtension(product.PostedFile.FileName);
                // Length of img_path
                HttpPostedFileBase postedFile = product.PostedFile;
                int length = postedFile.ContentLength;

                if (Extension.ToLower() == ".png" || Extension.ToLower() == ".jpg" || Extension.ToLower() == ".jpeg")
                {
                    if (length <= 1000000)
                    {
                        fileName  += Extension;
                        product.ProductImage = "~/Image/ProductImage/" + fileName;
                        fileName = Path.Combine(Server.MapPath("~/Image/ProductImage/"), fileName);
                        product.PostedFile.SaveAs(fileName);
                        if (ModelState.IsValid)
                        {
                            if (product.Category == "Men")
                            {
                                MenProduct data = new MenProduct()
                                {
                                    ProductName= product.ProductName,
                                    ProductImage = product.ProductImage,
                                    Brand = product.Brand,
                                    Description = product.Description,
                                    Status = product.Status,
                                    Price = product.Price,
                                    Category = product.SubCategory
                                };
                                
                                if (Product.InsertProduct(product))
                                {
                                    var d = product.GetProduct();
                                    foreach (var item in d)
                                    {
                                        data.ProductId = item.ProductId;

                                    }
                                    if (menProduct.InsertProduct(data))
                                    {
                                        ViewBag.Message = "<script>alert('Record inserted Successfully')</script>";
                                    }

                                    return RedirectToAction("Index", "Product");
                                }
                            }
                            else if (product.Category == "Women")
                            {
                                WomenModel womenProduct = new WomenModel()
                                {
                                    ProductName = product.ProductName,
                                    ProductImage = product.ProductImage,
                                    Brand = product.Brand,
                                    Description = product.Description,
                                    Status = product.Status,
                                    Price = product.Price,
                                    Category = product.SubCategory
                                };
                                if (Product.InsertProduct(product))
                                {
                                    var d = product.GetProduct();
                                    foreach (var item in d)
                                    {
                                        womenProduct.ProductId = item.ProductId;

                                    }
                                    if (womenProduct.InsertProduct(womenProduct))
                                    {
                                        ViewBag.Message = "<script>alert('Record inserted Successfully')</script>";
                                    }

                                    return RedirectToAction("Index", "Product");
                                }
                            }
                            else if (product.Category == "Electronics")
                            {
                                ElectronicsModel ElectronicsProduct = new ElectronicsModel()
                                {
                                    ProductName = product.ProductName,
                                    ProductImage = product.ProductImage,
                                    Brand = product.Brand,
                                    Description = product.Description,
                                    Status = product.Status,
                                    Price = product.Price,
                                    Category = product.SubCategory
                                };
                                if (Product.InsertProduct(product) == true)
                                {
                                    var d = product.GetProduct();
                                    foreach (var item in d)
                                    {
                                        ElectronicsProduct.ProductId = item.ProductId;

                                    }
                                    if (ElectronicsProduct.InsertProduct(ElectronicsProduct))
                                    {
                                        ViewBag.Message = "<script>alert('Record inserted Successfully')</script>";
                                    }

                                    return RedirectToAction("Index", "Product");
                                }
                            }

                        }
                        else
                        {
                            ViewBag.Message = "<script>alert('Record not inserted...')</script>";
                        }
                    }
                    else
                    {
                        ViewBag.SizeMessage = "Image Size Is More Than 1MB!";
                    }
                }
                else
                {
                    ViewBag.ExtensionMessage = "Image Is Not Supported!";
                }
            }
            else
            {
                ModelState.AddModelError("", "Please select a file.");
            }

            return View();
        }

        public ActionResult EditProduct(int id)
        {
            if (Session["UserRole"]?.ToString() == "Admin")
            {
                var productData = Product.GetProduct().FirstOrDefault(model => model.ProductId == id);
                if (productData == null)
                {
                    return View(); // Return 404 if product doesn't exist
                }
                return View(productData);
            }
            return RedirectToAction("CustomerDashboaed", "Home");
        }
        [HttpPost]
        public ActionResult EditProduct(Products product)
        {
            if (product.PostedFile.FileName != null && product.PostedFile.ContentLength > 0)
            {
                string fileName = Path.GetFileNameWithoutExtension(product.PostedFile.FileName);
                string Extension = Path.GetExtension(product.PostedFile.FileName);
                // Length of img_path
                HttpPostedFileBase postedFile = product.PostedFile;
                int length = postedFile.ContentLength;

                if (Extension.ToLower() == ".png" || Extension.ToLower() == ".jpg" || Extension.ToLower() == ".jpeg")
                {
                    if (length <= 1000000)
                    {
                        fileName += Extension;
                        product.ProductImage = "~/Image/ProductImage/" + fileName;
                        fileName = Path.Combine(Server.MapPath("~/Image/ProductImage/"), fileName);
                        product.PostedFile.SaveAs(fileName);
                        if (ModelState.IsValid)
                        {
                            if (product.Category == "Men")
                            {
                                MenProduct data = new MenProduct()
                                {
                                    ProductId = product.ProductId,
                                    ProductName = product.ProductName,
                                    ProductImage = product.ProductImage,
                                    Brand = product.Brand,
                                    Description = product.Description,
                                    Status = product.Status,
                                    Price = product.Price,
                                    Category = product.SubCategory
                                 };
                                if (menProduct.UpdateProduct(data) && Product.UpdateProduct(product))
                                {
                                    ViewBag.Message = "<script>alert('Record Updated Successfully')</script>";

                                    return RedirectToAction("Index", "Product");
                                }
                                else
                                {
                                    ViewBag.Message = "<script>alert('Record not Updated Successfully')</script>";
                                }
                            }
                            else if (product.Category == "Women")
                            {
                                WomenModel data = new WomenModel()
                                {
                                    ProductId = product.ProductId,
                                    ProductName = product.ProductName,
                                    ProductImage = product.ProductImage,
                                    Brand = product.Brand,
                                    Description = product.Description,
                                    Status = product.Status,
                                    Price = product.Price,
                                    Category = product.SubCategory
                                };
                                if (womenProduct.UpdateProduct(data) && Product.UpdateProduct(product))
                                {
                                    ViewBag.Message = "<script>alert('Record Updated Successfully')</script>";

                                    return RedirectToAction("Index", "Product");
                                }
                                else
                                {
                                    ViewBag.Message = "<script>alert('Record not Updated Successfully')</script>";
                                }
                            }
                            else if (product.Category == "Electronics")
                            {
                                ElectronicsModel data = new ElectronicsModel()
                                {
                                    ProductId = product.ProductId,
                                    ProductName = product.ProductName,
                                    ProductImage = product.ProductImage,
                                    Brand = product.Brand,
                                    Description = product.Description,
                                    Status = product.Status,
                                    Price = product.Price,
                                    Category = product.SubCategory
                                };
                                if (electronicsProduct.UpdateProduct(data) && Product.UpdateProduct(product))
                                {
                                    ViewBag.Message = "<script>alert('Record Updated Successfully')</script>";

                                    return RedirectToAction("Index", "Product");
                                }
                                else
                                {
                                    ViewBag.Message = "<script>alert('Record not Updated Successfully')</script>";
                                }
                            }
                        }
                        else
                        {
                            ViewBag.Message = "<script>alert('Record not inserted...')</script>";
                        }
                    }
                    else
                    {
                        ViewBag.SizeMessage = "Image Size Is More Than 1MB!";
                    }
                }
                else
                {
                    ViewBag.ExtensionMessage = "Image Is Not Supported!";
                }
            }
            else
            {
                ModelState.AddModelError("", "Please select a file.");
            }

            return View();
        }
        public ActionResult DeleteProduct(int id)
        {
            if (Session["UserRole"]?.ToString() == "Admin")
            {
                var productData = Product.GetProduct().FirstOrDefault(model => model.ProductId == id);
                if (productData == null)
                {
                    return View(); // Return 404 if product doesn't exist
                }
                return View(productData);
            }
            return RedirectToAction("CustomerDashboard", "Home");
        }

        [HttpPost]
        public ActionResult DeleteProduct(int ProductId, string Category)
        {
            if (Session["UserRole"]?.ToString() == "Admin")
            {
                if (Category == "Men")
                {
                    if (menProduct.DeletetProduct(ProductId) && Product.DeletetProduct(ProductId))
                    {
                        return RedirectToAction("Index", "Product");
                    }
                }
                else if (Category == "Women")
                {
                    if (womenProduct.DeletetProduct(ProductId) && Product.DeletetProduct(ProductId))
                    {
                        return RedirectToAction("Index", "Product");
                    }
                }
                else if (Category == "Electronics")
                {
                    if (electronicsProduct.DeletetProduct(ProductId) && Product.DeletetProduct(ProductId))
                    {
                        return RedirectToAction("Index", "Product");
                    }
                }

            }
            return View();
        }

        // only Admin can navigate from this Action

        public ActionResult DetailsProduct(int id)
        {
            if (Session["UserRole"]?.ToString() == "Admin")
            {
                var productData = Product.GetProduct().FirstOrDefault(model => model.ProductId == id);
                if (productData == null)
                {
                    return View(); // Return 404 if product doesn't exist
                }
                return View(productData);
            }
            return RedirectToAction("CustomerDashboard", "Home");
        }

        // Only Authorize Users can navigate from this Action -> when click on product card

        public ActionResult ProductDetails(int id)
        {
            if (Session["UserRole"]?.ToString() == "Admin" || Session["UserRole"]?.ToString() == "Customer")
            {
                var productData = Product.GetProduct().FirstOrDefault(model => model.ProductId == id);
                if (productData == null)
                {
                    return HttpNotFound(); // Return 404 if product doesn't exist
                }
                return View(productData);
            }
            return RedirectToAction("CustomerDashboard", "Home");
        }

        public ActionResult Checkout(int id)
        {
            UserModel user = new UserModel ();
            if (Session["UserRole"]?.ToString() == "Admin" || Session["UserRole"]?.ToString() == "Customer")
            {
                int Id = Convert.ToInt32( Session["UserId"]);
                var productData = Product.GetProduct().FirstOrDefault(model => model.ProductId == id);
                var UserData = user.GetUser().FirstOrDefault(model => model.UserID == Id);

                var data = new UserProductModel
                {
                    Products = productData,
                    UserModel = UserData,
                    quantity = 1
                };
                if (data == null)
                {
                    return HttpNotFound(); // Return 404 if product doesn't exist
                }
                return View(data);
            }
            return RedirectToAction("CustomerDashboard", "Home");
        }
    }

   
}