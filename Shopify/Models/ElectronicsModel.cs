using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ECommerce_Project.Models
{
    public class ElectronicsModel
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

        public List<ElectronicsModel> GetProduct()
        {
            List<ElectronicsModel> productList = new List<ElectronicsModel>();

            using (SqlConnection con = new SqlConnection(cs))
            {
                string query = "SELECT * FROM ElectronicsProducts_tbl";
                SqlCommand sqlCommand = new SqlCommand(query, con);
                    con.Open();
                    using (SqlDataReader dr = sqlCommand.ExecuteReader())
                    {
                        while (dr.Read() == true)
                        {
                            ElectronicsModel product = new ElectronicsModel();

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

            return productList;
        }

        // Insert product details of men 
        public bool InsertProduct(ElectronicsModel ElectronicsProduct)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                string query = "INSERT INTO ElectronicsProducts_tbl(ProductId,ProductName,ProductImage,Description,Category,Brand,Status,Price) VALUES(@ProductId,@ProductName,@ProductImage,@Description,@Category,@Brand,@Status,@Price)";
                SqlCommand sqlCommand = new SqlCommand(query, con);
                sqlCommand.Parameters.AddWithValue("@ProductId", ElectronicsProduct.ProductId);
                sqlCommand.Parameters.AddWithValue("@ProductName", ElectronicsProduct.ProductName);
                sqlCommand.Parameters.AddWithValue("@ProductImage", ElectronicsProduct.ProductImage);
                sqlCommand.Parameters.AddWithValue("@Description", ElectronicsProduct.Description);
                sqlCommand.Parameters.AddWithValue("@Category", ElectronicsProduct.Category);
                sqlCommand.Parameters.AddWithValue("@Brand", ElectronicsProduct.Brand);
                sqlCommand.Parameters.AddWithValue("@Status", ElectronicsProduct.Status);
                sqlCommand.Parameters.AddWithValue("@Price", ElectronicsProduct.Price);
                con.Open();
                int row = sqlCommand.ExecuteNonQuery();
                return row > 0;

            }

        }

        // Update product details of men 
        public bool UpdateProduct(ElectronicsModel ElectronicsProduct)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                string query = "UPDATE ElectronicsProducts_tbl SET ProductName=@ProductName,ProductImage=@ProductImage,Description=@Description,Category=@Category,Brand=@Brand,Status=@Status,Price=@Price WHERE ProductId=@ProductId";
                SqlCommand sqlCommand = new SqlCommand(query, con);

                sqlCommand.Parameters.AddWithValue("@ProductId", ElectronicsProduct.ProductId);
                sqlCommand.Parameters.AddWithValue("@ProductName", ElectronicsProduct.ProductName);
                sqlCommand.Parameters.AddWithValue("@ProductImage", ElectronicsProduct.ProductImage);
                sqlCommand.Parameters.AddWithValue("@Description", ElectronicsProduct.Description);
                sqlCommand.Parameters.AddWithValue("@Category", ElectronicsProduct.Category);
                sqlCommand.Parameters.AddWithValue("@Brand", ElectronicsProduct.Brand);
                sqlCommand.Parameters.AddWithValue("@Status", ElectronicsProduct.Status);
                sqlCommand.Parameters.AddWithValue("@Price", ElectronicsProduct.Price);
                con.Open();
                int row = sqlCommand.ExecuteNonQuery();
                return row > 0;

            }

        } // Delete product details of men 
        public bool DeletetProduct(int id)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                string query = "DELETE FROM ElectronicsProducts_tbl WHERE ProductId = @ProductId ";
                SqlCommand sqlCommand = new SqlCommand(query, con);
                sqlCommand.Parameters.AddWithValue("@ProductId", id);
                con.Open();
                int row = sqlCommand.ExecuteNonQuery();
                return row > 0;

            }

        }
    }
}
