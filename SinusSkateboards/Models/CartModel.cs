using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SinusSkateboards.Models
{
    public class CartModel
    {
        //[Key]
        //public int CartId { get; set; }
        public List<ProductModel> Products { get; set; }
        public int TotalPrice { get; set; }

        public CartModel(){ }

        public CartModel(List<ProductModel> products, int totalPrice)
        {
            Products = products;
            TotalPrice = totalPrice;
        }
    }
}
