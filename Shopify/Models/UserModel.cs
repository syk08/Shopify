using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ECommerce_Project.Models
{
    public class UserModel
    {
        public int UserID { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }

        public bool RememberMe { get; set; }

        private readonly string cs = ConfigurationManager.ConnectionStrings["EC_DB"].ConnectionString;

        // Register User (Sign Up)
        public bool RegisterUser()
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                string query = "INSERT INTO Users (FullName, Email, Password, Role) VALUES (@FullName, @Email, @Password, @Role); SELECT SCOPE_IDENTITY();";

                using (SqlCommand sqlCommand = new SqlCommand(query, con))
                {
                    sqlCommand.Parameters.AddWithValue("@FullName", FullName);
                    sqlCommand.Parameters.AddWithValue("@Email", Email);
                    sqlCommand.Parameters.AddWithValue("@Password", Password);
                    sqlCommand.Parameters.AddWithValue("@Role", Role);

                    con.Open();

                    // ExecuteScalar() returns the first column of the first row (UserId)
                    object result = sqlCommand.ExecuteScalar();

                    if (result != null)
                    {
                        UserID = Convert.ToInt32(result);
                        return true;
                    }

                    return false;
                }
            }
        }


        // Validate User Login
        public bool ValidateUser()
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                string query = "SELECT Role FROM Users WHERE Email = @Email AND Password = @Password";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Email", Email);
                cmd.Parameters.AddWithValue("@Password", Password);

                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                 
                    Role = dr["Role"].ToString();
                    return true;
                }
                return false;
            }
        }
        public List<UserModel> GetUser()
        {
            List<UserModel> userList = new List<UserModel>();
            using (SqlConnection con = new SqlConnection(cs))
            {
                string query = "SELECT * FROM Users ";
                SqlCommand cmd = new SqlCommand(query, con);

                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    UserModel u = new UserModel();
                    u.UserID = dr.GetInt32(0);
                    u.FullName = dr.GetString(1);
                    u.Email = dr.GetString(2);
                    u.Password = dr.GetString(3);
                    u.Role = dr.GetString(4);
                    userList.Add(u);
                }
                return userList;
            }
        }

    }
}