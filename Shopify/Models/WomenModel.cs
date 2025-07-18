using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ECommerce_Project.Models
{
    public class WomenModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductImage { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string Brand { get; set; }
        public string Status { get; set; }
        public float Price { get; set; }

        private readonly string cs = ConfigurationManager.ConnectionStrings["EC_DB"].ConnectionString;

        public List<WomenModel> GetProduct()
        {
            List<WomenModel> productList = new List<WomenModel>();

            using (SqlConnection con = new SqlConnection(cs))
            {
                string query = "SELECT * FROM WomenProducts_tbl";
                SqlCommand sqlCommand = new SqlCommand(query, con);

                try
                {
                    con.Open();
                    using (SqlDataReader dr = sqlCommand.ExecuteReader())
                    {
                        while (dr.Read() == true)
                        {
                            WomenModel product = new WomenModel();

                            product.ProductId = dr.GetInt32(0);
                            product.ProductName = dr.GetString(1);
                            product.Category = dr.GetString(2);
                            product.Brand = dr.GetString(3);
                            product.Description = dr.GetString(4);
                            product.Price = (float)dr.GetDouble(5);
                            product.Status = dr.GetString(6);
                            product.ProductImage = dr.GetString(7);

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
        public bool InsertProduct(WomenModel women)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                string query = "INSERT INTO WomenProducts_tbl(ProductId,ProductName,ProductImage,Description,Category,Brand,Status,Price) VALUES(@ProductId,@ProductName,@ProductImage,@Description,@Category,@Brand,@Status,@Price)";
                SqlCommand sqlCommand = new SqlCommand(query, con);
                sqlCommand.Parameters.AddWithValue("@ProductId", women.ProductId);
                sqlCommand.Parameters.AddWithValue("@ProductName", women.ProductName);
                sqlCommand.Parameters.AddWithValue("@ProductImage", women.ProductImage);
                sqlCommand.Parameters.AddWithValue("@Description", women.Description);
                sqlCommand.Parameters.AddWithValue("@Category", women.Category);
                sqlCommand.Parameters.AddWithValue("@Brand", women.Brand);
                sqlCommand.Parameters.AddWithValue("@Status", women.Status);
                sqlCommand.Parameters.AddWithValue("@Price", women.Price);
                con.Open();
                int row = sqlCommand.ExecuteNonQuery();
                return row > 0;

            }

        }

        // Update product details of men 
        public bool UpdateProduct(WomenModel women)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                string query = "UPDATE WomenProducts_tbl SET ProductName=@ProductName,ProductImage=@ProductImage,Description=@Description,Category=@Category,Brand=@Brand,Status=@Status,Price=@Price WHERE ProductId=@ProductId";
                SqlCommand sqlCommand = new SqlCommand(query, con);

                sqlCommand.Parameters.AddWithValue("@ProductId", women.ProductId);
                sqlCommand.Parameters.AddWithValue("@ProductName", women.ProductName);
                sqlCommand.Parameters.AddWithValue("@ProductImage", women.ProductImage);
                sqlCommand.Parameters.AddWithValue("@Description", women.Description);
                sqlCommand.Parameters.AddWithValue("@Category", women.Category);
                sqlCommand.Parameters.AddWithValue("@Brand", women.Brand);
                sqlCommand.Parameters.AddWithValue("@Status", women.Status);
                sqlCommand.Parameters.AddWithValue("@Price", women.Price);
                con.Open();
                int row = sqlCommand.ExecuteNonQuery();
                return row > 0;

            }

        } // Delete product details of men 
        public bool DeletetProduct(int id)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                string query = "DELETE FROM WomenProducts_tbl WHERE ProductId = @ProductId ";
                SqlCommand sqlCommand = new SqlCommand(query, con);
                sqlCommand.Parameters.AddWithValue("@ProductId", id);
                con.Open();
                int row = sqlCommand.ExecuteNonQuery();
                return row > 0;

            }

        }
    }
}
