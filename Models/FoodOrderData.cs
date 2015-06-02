using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelNWT.Models
{
    public class FoodOrderData
    {
        public int idfood_order { get; set; }
        public string order_name { get; set; }
        public System.DateTime oder_date { get; set; }
        public int amount { get; set; }
        public double order_price { get; set; }
        public int user_iduser { get; set; }
        public int food_menu_idfood { get; set; }

        public virtual food_menu food_menu { get; set; }
        public virtual user user { get; set; }
    }
}