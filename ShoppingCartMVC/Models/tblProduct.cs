//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ShoppingCartMVC.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblProduct
    {
        public tblProduct()
        {
            this.tblOrders = new HashSet<tblOrder>();
        }
    
        public int ProID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Nullable<int> Unit { get; set; }
        public string Image { get; set; }
        public Nullable<int> CatId { get; set; }
    
        public virtual tblCategory tblCategory { get; set; }
        public virtual ICollection<tblOrder> tblOrders { get; set; }
    }
}
