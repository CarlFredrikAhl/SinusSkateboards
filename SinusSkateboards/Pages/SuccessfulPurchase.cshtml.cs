using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using SinusSkateboards.Database;
using SinusSkateboards.Models;

namespace SinusSkateboards.Pages
{
    public class SuccessfulPurchaseModel : PageModel
    {
        private readonly AppDbContext database;

        [BindProperty]
        public Checkout Checkout { get; set; }
       
        public Cart Cart { get; set; }
       
        public Order Order { get; set; }

        public SuccessfulPurchaseModel(AppDbContext context)
        {
            database = context;
        }

        public void OnGet(int checkoutId)
        {
            //Get cookie products from cart
            Cart.Products = new List<Product>();

            List<Product> cookieProducts = new List<Product>();

            string stringProducts = HttpContext.Session.GetString("cart_items");

            //Cookie products exists in the cart already
            if (stringProducts != null)
            {
                cookieProducts = JsonConvert.DeserializeObject<List<Product>>(stringProducts);
            }

            Cart.Products = cookieProducts.ToList();

            //Save order to database
            Order = new Order(Checkout.CheckoutId, Cart.Products, DateTime.Now);
            database.Orders.Add(Order);

            Checkout = database.Checkouts.Where(checkout => checkout.CheckoutId == checkoutId).FirstOrDefault();
        }
    }
}
