//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HotelNWT
{
    using System;
    using System.Collections.Generic;
    
    public partial class food_menu
    {
        public food_menu()
        {
            this.food_order = new HashSet<food_order>();
        }
    
        public int idfood { get; set; }
        public string name { get; set; }
        public double price { get; set; }
        public string description { get; set; }
        public int amount_available { get; set; }
    
        public virtual ICollection<food_order> food_order { get; set; }
    }
}
