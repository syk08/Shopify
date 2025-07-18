using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ECommerce_Project.Models
{
    public class UserProductModel
    {
        public UserModel UserModel { get; set; }

        public Products Products { get; set; }
        public DateTime time;
        public int quantity;
    }
}