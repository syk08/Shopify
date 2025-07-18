using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ECommerce_Project.Models
{
    public class Products
    {
        public int ProductId { get; set; }
        [Display(Name = "Product Name")]

        [Required]
        public string ProductName { get; set; }
        [Display(Name = "Upload File")]
        public string ProductImage { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Category { get; set; }
        [Required]
        public string SubCategory { get; set; }
        [Required]
        public string Brand { get; set; }
        [Required]
        public string Status { get; set; }
        [Required]
        public float Price { get; set; }

        public HttpPostedFileBase PostedFile { get; set; }

        private readonly string cs = ConfigurationManager.ConnectionStrings["EC_DB"].ConnectionString;

        public List<Products> GetProduct()
        {
            List<Products> productList = new List<Products>();

            using (SqlConnection con = new SqlConnection(cs))
            {
                string query = "SELECT * FROM Products_tbl";
                SqlCommand sqlCommand = new SqlCommand(query, con);

                try
                {
                    con.Open();
                    using (SqlDataReader dr = sqlCommand.ExecuteReader())
                    {
                        while (dr.Read() == true)
                        {
                            Products product = new Products();

                            product.ProductId = dr.GetInt32(0);
                            product.ProductName = dr.GetString(1);
                            product.Category = dr.GetString(2);
                            product.SubCategory = dr.GetString(3);
                            product.Brand = dr.GetString(4);
                            product.Description = dr.GetString(5);
                            product.Price = (float)dr.GetDouble(6);
                            product.Status = dr.GetString(7);
                            product.ProductImage = dr.GetString(8);

                            productList.Add(product);
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Log the error or handle it properly
                    Console.WriteLine("Error: " + ex.Message);
                }
            }

            return productList;
        }

        // Insert product details of men 
        public bool InsertProduct(Products products)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                string query = "INSERT INTO Products_tbl(ProductName,ProductImage,Description,Category,SubCategory,Brand,Status,Price) VALUES(@ProductName,@ProductImage,@Description,@Category,@SubCategory,@Brand,@Status,@Price)";
                SqlCommand sqlCommand = new SqlCommand(query, con);
                sqlCommand.Parameters.AddWithValue("@ProductName", products.ProductName);
                sqlCommand.Parameters.AddWithValue("@ProductImage", products.ProductImage);
                sqlCommand.Parameters.AddWithValue("@Description", products.Description);
                sqlCommand.Parameters.AddWithValue("@Category", products.Category);
                sqlCommand.Parameters.AddWithValue("@SubCategory", products.SubCategory);
                sqlCommand.Parameters.AddWithValue("@Brand", products.Brand);
                sqlCommand.Parameters.AddWithValue("@Status", products.Status);
                sqlCommand.Parameters.AddWithValue("@Price", products.Price);
                con.Open();
                int row = sqlCommand.ExecuteNonQuery();
                return row > 0;

            }

        }

        // Update product details of men 
        public bool UpdateProduct(Products products)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                string query = "UPDATE Products_tbl SET ProductName=@ProductName,ProductImage=@ProductImage,Description=@Description,Category=@Category,SubCategory=@SubCategory,Brand=@Brand,Status=@Status,Price=@Price WHERE ProductId=@ProductId";
                SqlCommand sqlCommand = new SqlCommand(query, con);

                sqlCommand.Parameters.AddWithValue("@ProductId", products.ProductId);
                sqlCommand.Parameters.AddWithValue("@ProductName", products.ProductName);
                sqlCommand.Parameters.AddWithValue("@ProductImage", products.ProductImage);
                sqlCommand.Parameters.AddWithValue("@Description", products.Description);
                sqlCommand.Parameters.AddWithValue("@Category", products.Category);
                sqlCommand.Parameters.AddWithValue("@SubCategory", products.SubCategory);
                sqlCommand.Parameters.AddWithValue("@Brand", products.Brand);
                sqlCommand.Parameters.AddWithValue("@Status", products.Status);
                sqlCommand.Parameters.AddWithValue("@Price", products.Price);
                con.Open();
                int row = sqlCommand.ExecuteNonQuery();
                return row > 0;

            }

        } // Delete product details of men 
        public bool DeletetProduct(int id)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                string query = "DELETE FROM Products_tbl WHERE ProductId = @ProductId ";
                SqlCommand sqlCommand = new SqlCommand(query, con);
                sqlCommand.Parameters.AddWithValue("@ProductId", id);
                con.Open();
                int row = sqlCommand.ExecuteNonQuery();
                return row > 0;

            }

        }


    }
}
