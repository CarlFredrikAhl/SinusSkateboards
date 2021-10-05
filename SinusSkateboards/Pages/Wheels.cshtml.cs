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
    public class WheelsModel : PageModel
    {
        private readonly AppDbContext database;

        public List<Product> Products { get; set; }

        public int ItemsInCart { get; set; }

        [BindProperty]
        public string SearchString { get; set; }

        public WheelsModel(AppDbContext context)
        {
            database = context;
        }

        public void OnGet()
        {
            //Add the products to the database if there are no ones already
            if (database.Products.Where(product => product.Title.Contains("Wheel")).ToList().Count == 0)
            {
                Products = new List<Product>()
                {
                    new Product("sinus-wheel-rocket.png", "Wheel (rocket)", "Four rocket skate wheels"
                    , "White/Red", 20),
                    new Product("sinus-wheel-spinner.png", "Wheel (spinner)", "Four white skate wheels"
                    , "White", 20),
                    new Product("sinus-wheel-wave.png", "Wheel (wave)", "Four wave skate wheels"
                    , "White/Black", 20),
                };

                foreach (var product in Products)
                {
                    database.Products.Add(product);
                }

                database.SaveChanges();
            
            }
            else
            {
                Products = database.Products.Where(product => product.Title.Contains("Wheel")).ToList();
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
            return RedirectToPage("/ChosenProduct", new { id = productId });
        }

        public IActionResult OnPostSearch()
        {
            return RedirectToPage("/Search", new { search = SearchString });
        }
    }
}
