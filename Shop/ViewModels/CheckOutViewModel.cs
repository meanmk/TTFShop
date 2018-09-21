using Shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shop.ViewModels
{
    public class CheckOutViewModel
    {
       
        public Order Order { get; set; }
        public OrderDetail OrderDetail { get; set; }
        public Product Product { get; set; }

    }
}