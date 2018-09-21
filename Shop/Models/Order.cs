using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Shop.Models
{
    public partial class Order
    {
        [Key]
        public int OrderId { get; set; }

        [Required(ErrorMessage ="First Name is required.")]
        [DisplayName("First Name")]
        [StringLength(100)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required.")]
        [DisplayName("Last Name")]
        [StringLength(100)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email address is required.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone number is required.")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Address is required.")]
        public string Address { get; set; }

        [Required(ErrorMessage = "City is required.")]
        public string City { get; set; }

        [Required(ErrorMessage = "State is required.")]
        public string State { get; set; }

        [Required(ErrorMessage = "Postal Code is required.")]
        [DisplayName("Postal Code")]
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public decimal Total { get; set; }
        public DateTime OrderDate { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }

    }
}