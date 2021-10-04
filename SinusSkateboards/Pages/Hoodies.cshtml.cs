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
    public class HoodiesModel : PageModel
    {
        private readonly AppDbContext database;

        public List<Product> Products { get; set; }

        public int ItemsInCart { get; set; }

        public HoodiesModel(AppDbContext context)
        {
            database = context;
        }

        public void OnGet()
        {
            //Add the products to the database if there are no ones already
            if (database.Products.Where(product => product.Title.Contains("Hoodie")).ToList().Count == 0)
            {
                Products = new List<Product>()
                {
                    new Product("hoodie-ash.png", "Hoodie (ash)", "Grey skate hoodie"
                    , "Grey", 35),
                    new Product("hoodie-fire.png", "Hoodie (fire)", "Red skate hoodie"
                    , "Red", 35),
                    new Product("hoodie-green.png", "Hoodie (green)", "Green skate hoodie"
                    , "Green", 35),
                    new Product("hoodie-ocean.png", "Hoodie (ocean)", "Blue skate hoodie"
                    , "Blue", 35),
                    new Product("hoodie-purple.png", "Hoodie (purple)", "Purple skate hoodie"
                    , "Purple", 35),
                };

                foreach (var product in Products)
                {
                    database.Products.Add(product);
                }

                database.SaveChanges();
            
            } else
            {
                Products = database.Products.Where(product => product.Title.Contains("Hoodie")).ToList();
            }

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
        public IActionResult OnPost(int productId)
        {
            //Clicked product
            var product = database.Products.Where(product => product.ProductId == productId).FirstOrDefault();

            //Save to session cookie
            List<Product> cookieProducts = new List<Product>();

            string stringProducts = HttpContext.Session.GetString("cart_items");

            //Cookie products exists in the cart already
            if (stringProducts != null)
            {
                cookieProducts = JsonConvert.DeserializeObject<List<Product>>(stringProducts);
            }

            cookieProducts.Add(product);

            stringProducts = JsonConvert.SerializeObject(cookieProducts);

            HttpContext.Session.SetString("cart_items", stringProducts);

            return RedirectToPage("/Hoodies");
        }
    }
}
