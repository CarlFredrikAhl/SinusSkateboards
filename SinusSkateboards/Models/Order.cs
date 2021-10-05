using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SinusSkateboards.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; } //Primary key
        public int CheckoutId { get; set; } //Foreign key
        public int OrderNumber { get; set; }
        public List<Product> Products { get; set; }
        public DateTime Date { get; set; }

        public Order() { }

        public Order(int checkoutId, List<Product> products, DateTime date)
        {
            CheckoutId = checkoutId;
            Products = products;
            Date = date;
            OrderNumber = checkoutId + new Random().Next(0, 100) + new Random().Next(0, 100)
                + new Random().Next(0, 100) + new Random().Next(0, 100) + new Random().Next(0, 100)
                + new Random().Next(0, 100) + new Random().Next(0, 100) + new Random().Next(0, 100);
        }
    }
}
