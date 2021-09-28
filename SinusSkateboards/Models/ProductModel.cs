using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SinusSkateboards.Models
{
    public class ProductModel
    {
        [Key]
        public int ProductId { get; set; } //Primary key
        public int CartId { get; set; } //Foreign key
        public string Image { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Color { get; set; }
        public int Price { get; set; }
    }
}
