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
        public int TotalPrice 
        {
            get 
            {
                int curTotalPrice = 0;

                if(Products.Count > 0)
                {
                    foreach (var product in Products)
                    {
                        curTotalPrice += product.Price;
                    }

                } else
                {
                    curTotalPrice = 0;
                }

                return curTotalPrice;
            }

            set { TotalPrice = value; } 
        
        }

        public Cart()
        {
            Products = new List<Product>();
        }

        public Cart(List<Product> products, int totalPrice)
        {
            Products = products;
            TotalPrice = totalPrice;
        }
    }
}
