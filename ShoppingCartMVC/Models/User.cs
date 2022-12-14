using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace ShoppingCartMVC.Models
{
    public class User
    {
        [Required]
        public string MailID { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}