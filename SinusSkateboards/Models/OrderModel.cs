using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SinusSkateboards.Models
{
    public class OrderModel
    {
        [Key]
        public int OrderId { get; set; } //Primary key
        public int CheckoutId { get; set; } //Foreign key
        public int OrderNumber { get; set; }
        public List<ProductModel> Products { get; set; }
        public DateTime Date { get; set; }
    }
}
