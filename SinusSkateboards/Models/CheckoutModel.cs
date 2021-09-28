using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SinusSkateboards.Models
{
    public class CheckoutModel
    {
        [Key]
        public int CheckoutId { get; set; } //Primary key
        public int CartId { get; set; } //Foreign key
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string DeliveryAdress { get; set; }
        public int PhoneNumber { get; set; }
    }
}
