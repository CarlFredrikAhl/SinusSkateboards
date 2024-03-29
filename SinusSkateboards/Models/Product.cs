﻿using System;
using System.ComponentModel.DataAnnotations;

namespace SinusSkateboards.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; } //Primary key
        public int? OrderId { get; set; } //Foreign key
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
    }
}
