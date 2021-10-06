using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SinusSkateboards.Database;
using SinusSkateboards.Models;

namespace SinusSkateboards.Pages
{
    public class OrdersModel : PageModel
    {
        private readonly AppDbContext database;

        public List<Order> Orders { get; set; }

        public OrdersModel(AppDbContext context)
        {
            database = context;
        }

        public void OnGet()
        {
            Orders = database.Orders.ToList();

            foreach (var order in Orders)
            {
                order.Products = database.Products.Where(product => product.OrderId == order.OrderId).ToList();
            }
        }

        public Checkout GetCustomerData(Order order)
        {
            Checkout checkout = database.Checkouts.Where(checkout => checkout.CheckoutId == order.CheckoutId).FirstOrDefault();
            return checkout;
        }
    }
}
