using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SinusSkateboards.Models
{
    public class Checkout
    {
        [Key]
        public int CheckoutId { get; set; } //Primary key
        public int OrderId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string DeliveryAddress { get; set; }
        public int ?PhoneNumber { get; set; }
    }
}
