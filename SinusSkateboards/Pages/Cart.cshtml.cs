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
    public class CartModel : PageModel
    {
        private readonly AppDbContext database;

        [BindProperty]
        public List<Product> Products { get; set; }

        public int ItemsInCart { get; set; }

        public CartModel(AppDbContext context)
        {
            database = context;
        }

        public void OnGet()
        {

            //Check how many items in cart
            ItemsInCart = 0;

            List<Product> cookieProducts = new List<Product>();

            string stringProducts = HttpContext.Session.GetString("cart_items");

            //Cookie products exists in the cart already
            if (stringProducts != null)
            {
                cookieProducts = JsonConvert.DeserializeObject<List<Product>>(stringProducts);
            }

            foreach (var product in cookieProducts)
            {
                ItemsInCart++;
            }
        }

        //Method for adding item to the cart
        //Didn't work naming it to "OnPostAddToCart" and using asp-page-handler="AddToCart"
        //public IActionResult OnPost(int productId)
        //{
        //}
    }
}

