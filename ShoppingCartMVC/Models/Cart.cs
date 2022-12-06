using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingCartMVC.Models
{
    public class Cart
    {
        public int Proid { get; set; }
        public string Image { get; set; }
        public string Proname { get; set; }
        public int Qty { get; set; }
        public int Price { get; set; }
        public int Bill { get; set; }
    }
}