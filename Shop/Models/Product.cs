using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Shop.Models
{
    public class Product
    {
        public int Id { get; set; }

        [DisplayName("Product Name")]
        [StringLength(100)]
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }

    }
}