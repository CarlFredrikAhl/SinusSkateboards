using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SinusSkateboards.Models
{
    public class Cart
    {
        //[Key]
        //public int CartId { get; set; }
        public List<Product> Products { get; set; }
        public int TotalPrice { get; set; }

        public Cart(){ }

        public Cart(List<Product> products, int totalPrice)
        {
            Products = products;
            TotalPrice = totalPrice;
        }
    }
}
