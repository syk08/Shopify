using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ECommerce_Project.Models
{
    public class CartModel
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ImageUrl { get; set; }
        public float Price { get; set; }
        public int Quantity { get; set; }
        public string UserId { get; set; }

        string cs = ConfigurationManager.ConnectionStrings["EC_DB"].ConnectionString;

        public List<CartModel> GetCarts()
        {
            var cartList = new List<CartModel>();

            using (SqlConnection con = new SqlConnection(cs))
            {
                string query = "SELECT * FROM Cart_tbl";
                SqlCommand cmd = new SqlCommand(query, con);
                try
                {
                    con.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read() == true)
                        {
                            CartModel cartItem = new CartModel();

                            cartItem.Id = dr.GetInt32(0);
                            cartItem.ProductId = dr.GetInt32(1);
                            cartItem.ProductName = dr.GetString(2);
                            cartItem.ImageUrl = dr.GetString(3);
                            cartItem.Price = (float)dr.GetDouble(4);
                            cartItem.Quantity = dr.GetInt32(5);
                            cartItem.UserId = dr.GetString(6);

                            cartList.Add(cartItem);
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Log the error or handle it properly
                    Console.WriteLine("Error: " + ex.Message);
                }
                
            }

            return cartList;
        }

        public bool AddCarts(CartModel cartItem)
        {

            using (SqlConnection con = new SqlConnection(cs))
            {
                string query = "INSERT INTO Cart_tbl(ProductId,ProductName,ImageUrl,Price,Quantity,UserId) VALUES(@ProductId,@ProductName,@ProductImage,@Price,@Quantity,@UserId)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@ProductId", cartItem.ProductId);
                cmd.Parameters.AddWithValue("@ProductName", cartItem.ProductName);
                cmd.Parameters.AddWithValue("@ProductImage", cartItem.ImageUrl);
                cmd.Parameters.AddWithValue("@Price", cartItem.Price);
                cmd.Parameters.AddWithValue("@Quantity", cartItem.Quantity);
                cmd.Parameters.AddWithValue("@UserId", cartItem.UserId);
                con.Open();
                int row = cmd.ExecuteNonQuery();
                return row > 0;
            }

        }
        public bool DeleteCart(int id)
        {

            using (SqlConnection con = new SqlConnection(cs))
            {
                string query = "DELETE FROM Cart_tbl WHERE Id = @ProductId";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@ProductId", id);
                con.Open();
                int row = cmd.ExecuteNonQuery();
                return row > 0;
            }

        }
    }


}