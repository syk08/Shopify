using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ECommerce_Project.Models
{
    public class OrderModel
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int UserId { get; set; }
        public string ProductName { get; set; }
        public DateTime OrderTime { get; set; }
        public float ProductPrice { get; set; }
        public int ProductQuantity { get; set; }

        private readonly string cs = ConfigurationManager.ConnectionStrings["EC_DB"].ConnectionString;

        // Get Order Data from the database > Table : Order_tbl (Used for both Customers and Admin)
        public List<OrderModel> GetOrders()
        {
            var orders = new List<OrderModel>();
            using (SqlConnection con = new SqlConnection(cs))
            {
                string query = "SELECT * FROM Order_tbl";

                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        orders.Add(new OrderModel
                        {
                            OrderId = sdr.GetInt32(0),
                            ProductId = sdr.GetInt32(1),
                            UserId = sdr.GetInt32(2),
                            ProductName = sdr.GetString(3),
                            OrderTime = sdr.GetDateTime(4),
                            ProductPrice = (float)sdr.GetDouble(5),
                            ProductQuantity = sdr.GetInt32(6)

                        });
                    }
                }
            }
            return orders;
        }
        
        // Add Order Details into database > table: Ored_tbl , that the Customer place Order for the product 
        public bool AddOrder(UserProductModel orderItems)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                string query = "INSERT INTO Order_tbl(ProductId,UserId,ProductName,OrderTime,ProductPrice,ProductQuantity) VALUES(@ProductId,@UserId,@ProductName,@OrderTime,@ProductPrice,@ProductQuantity)";
                
                SqlCommand sqlCommand = new SqlCommand(query,con);
                sqlCommand.Parameters.AddWithValue("@ProductId", orderItems.Products.ProductId);
                sqlCommand.Parameters.AddWithValue("@UserId", orderItems.UserModel.UserID);
                sqlCommand.Parameters.AddWithValue("@ProductName", orderItems.Products.ProductName);
                sqlCommand.Parameters.AddWithValue("@OrderTime", orderItems.time);
                sqlCommand.Parameters.AddWithValue("@ProductPrice", orderItems.Products.Price);
                sqlCommand.Parameters.AddWithValue("@ProductQuantity", orderItems.quantity);
                
                con.Open();
                int row = sqlCommand.ExecuteNonQuery();
                return row > 0;

            }
           
        }
        // This method will use when Customer Cancle the order 
        public bool DeleteOrder(int id)
        {
            using (SqlConnection con = new SqlConnection())
            {
                string query = "DELETE FROM Order_tbl WHERE orderId = @orderId";

                SqlCommand sqlCommand = new SqlCommand(query, con);
                sqlCommand.Parameters.AddWithValue("@orderId", id);

                con.Open();
                int row = sqlCommand.ExecuteNonQuery();
                return row > 0;

            }

        }

    }
}