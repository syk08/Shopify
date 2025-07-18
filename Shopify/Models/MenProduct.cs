using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ECommerce_Project.Models
{
    public class MenProduct
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

        public List<MenProduct> GetProduct()
        {
            List<MenProduct> productList = new List<MenProduct>();

            using (SqlConnection con = new SqlConnection(cs))
            {
                string query = "SELECT * FROM MenProducts";
                SqlCommand sqlCommand = new SqlCommand(query, con);

                try
                {
                    con.Open();
                    using (SqlDataReader dr = sqlCommand.ExecuteReader())
                    {
                        while (dr.Read() == true)
                        {
                            MenProduct product = new MenProduct();

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
        public bool InsertProduct(MenProduct men)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                string query = "INSERT INTO MenProducts(ProductId,ProductName,ProductImage,Description,Category,Brand,Status,Price) VALUES(@ProductId,@ProductName,@ProductImage,@Description,@Category,@Brand,@Status,@Price)";
                SqlCommand sqlCommand = new SqlCommand(query, con);
                sqlCommand.Parameters.AddWithValue("@ProductId", men.ProductId);
                sqlCommand.Parameters.AddWithValue("@ProductName", men.ProductName);
                sqlCommand.Parameters.AddWithValue("@ProductImage", men.ProductImage);
                sqlCommand.Parameters.AddWithValue("@Description", men.Description);
                sqlCommand.Parameters.AddWithValue("@Category", men.Category);
                sqlCommand.Parameters.AddWithValue("@Brand", men.Brand);
                sqlCommand.Parameters.AddWithValue("@Status", men.Status);
                sqlCommand.Parameters.AddWithValue("@Price", men.Price);
                con.Open();
                int row = sqlCommand.ExecuteNonQuery();
                return row > 0;

            }

        }

        // Update product details of men 
        public bool UpdateProduct(MenProduct men)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                string query = "UPDATE MenProducts SET ProductName=@ProductName,ProductImage=@ProductImage,Description=@Description,Category=@Category,Brand=@Brand,Status=@Status,Price=@Price WHERE ProductId=@ProductId";
                SqlCommand sqlCommand = new SqlCommand(query, con);

                sqlCommand.Parameters.AddWithValue("@ProductId", men.ProductId);
                sqlCommand.Parameters.AddWithValue("@ProductName", men.ProductName);
                sqlCommand.Parameters.AddWithValue("@ProductImage", men.ProductImage);
                sqlCommand.Parameters.AddWithValue("@Description", men.Description);
                sqlCommand.Parameters.AddWithValue("@Category", men.Category);
                sqlCommand.Parameters.AddWithValue("@Brand", men.Brand);
                sqlCommand.Parameters.AddWithValue("@Status", men.Status);
                sqlCommand.Parameters.AddWithValue("@Price", men.Price);
                con.Open();
                int row = sqlCommand.ExecuteNonQuery();
                return row > 0;

            }

        } // Delete product details of men 
        public bool DeletetProduct(int id)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                string query = "DELETE FROM MenProducts WHERE ProductId = @ProductId ";
                SqlCommand sqlCommand = new SqlCommand(query, con);
                sqlCommand.Parameters.AddWithValue("@ProductId", id);
                con.Open();
                int row = sqlCommand.ExecuteNonQuery();
                return row > 0;

            }

        }
      
    }
}
