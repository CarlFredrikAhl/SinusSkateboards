using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

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

            StringBuilder sb = new StringBuilder();
            sb.Append(checkoutId);

            for (int i = 0; i < 7; i++)
            {
                int randomNr = new Random().Next(0, 10);
                sb.Append(randomNr);
            }

            //Unique because of checkoutId
            OrderNumber = int.Parse(sb.ToString());
        }
    }
}
