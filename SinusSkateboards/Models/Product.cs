using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace SinusSkateboards.Models
{
    public class Product : IEquatable<Product>
    {
        [Key]
        public int ProductId { get; set; } //Primary key
        public string Image { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Color { get; set; }
        public int Price { get; set; }

        public Product() { }

        public Product(string img, string title, string desc,
            string color, int price)
        {
            Image = img;
            Title = title;
            Description = desc;
            Color = color;
            Price = price;
        }

        public bool Equals(Product other)
        {
            return Title.Equals(other.Title);
        }
        public int GetHashCode(Product product)
        {
            return (product.Title).GetHashCode();
        }
    }
}
