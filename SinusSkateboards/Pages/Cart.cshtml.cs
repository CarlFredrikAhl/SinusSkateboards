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
        public Cart Cart { get; set; }

        [BindProperty]
        public List<int> SameProductId { get; set; }

        public CartModel(AppDbContext context)
        {
            database = context;
        }

        public void OnGet()
        {
            Cart = new Cart();

            SameProductId = new List<int>();

            List<Product> cookieProducts = new List<Product>();

            string stringProducts = HttpContext.Session.GetString("cart_items");

            //Cookie products exists in the cart already
            if (stringProducts != null)
            {
                cookieProducts = JsonConvert.DeserializeObject<List<Product>>(stringProducts);
            }

            Cart.Products = cookieProducts.Distinct().ToList();
        }

        //Metod for deleating product from cart
        public IActionResult OnPostDelete(int productId)
        {
            List<Product> cookieProducts = new List<Product>();

            string stringProducts = HttpContext.Session.GetString("cart_items");

            //Cookie products exists in the cart already
            if (stringProducts != null)
            {
                cookieProducts = JsonConvert.DeserializeObject<List<Product>>(stringProducts);
            }

            Product productToRemove = cookieProducts.Where(product => product.ProductId == productId).FirstOrDefault();

            cookieProducts.Remove(productToRemove);

            stringProducts = JsonConvert.SerializeObject(cookieProducts);

            HttpContext.Session.SetString("cart_items", stringProducts);

            if(cookieProducts.Any())
            {
                return RedirectToPage("/Cart");
            
            } else
            {
                //Try to change this to the previous page if I have time
                return RedirectToPage("/Index");
            }
        }
    }
}

