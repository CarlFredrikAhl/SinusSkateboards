﻿using System.ComponentModel.DataAnnotations;

namespace SinusSkateboards.Models
{
    public class Checkout
    {
        [Key]
        public int CheckoutId { get; set; } //Primary key
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string DeliveryAddress { get; set; }
        public int? PhoneNumber { get; set; }
    }
}
