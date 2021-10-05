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
    public class BoardsModel : PageModel
    {
        private readonly AppDbContext database;

        public List<Product> Products { get; set; }

        [BindProperty]
        public string SearchString { get; set; }

        public int ItemsInCart { get; set; }

        public BoardsModel(AppDbContext context)
        {
            database = context;
        }

        public void OnGet()
        {
            //Add the products to the database if there are no ones already
            if (database.Products.Where(product => product.Title.Contains("Skateboard")).ToList().Count == 0)
            {
                Products = new List<Product>()
                {
                    new Product("sinus-skateboard-eagle.png", "Skateboard (eagle)", "Board with eagle print"
                    , "Black/White/Yellow", 80),
                    new Product("sinus-skateboard-fire.png", "Skateboard (fire)", "Board with fire print"
                    , "Orange/Brown", 75),
                    new Product("sinus-skateboard-gretasfury.png", "Skateboard (Gretas fury)", 
                    "Skateboard with Greta Thunberg print", "Black/Brown", 90),
                    new Product("sinus-skateboard-ink.png", "Skateboard (ink)", "Board with ink print"
                    , "Wood/Black", 65),
                    new Product("sinus-skateboard-logo.png", "Skateboard (logo)"
                    , "Board with only our beautiful logo", "Wood", 60),
                    new Product("sinus-skateboard-northern_lights.png", "Skateboard (northern lights)"
                    , "Board with northen lights print", "Green/Blue/Black", 100),
                    new Product("sinus-skateboard-plastic.png", "Skateboard (plastic)", "Board with plastic print"
                    , "Mixed plastic colors", 60),
                    new Product("sinus-skateboard-polar.png", "Skateboard (polar)", "Board with polar bear print"
                    , "Black/Wood", 75),
                    new Product("sinus-skateboard-purple.png", "Skateboard (purple)", "Purple board"
                    , "Purple", 95),
                    new Product("sinus-skateboard-yellow.png", "Skateboard (yellow)", "Yellow board"
                    , "Yellow", 85),
                };


                foreach (var product in Products)
                {
                    database.Products.Add(product);
                }

                database.SaveChanges();
            
            }
            else
            {
                Products = database.Products.Where(product => product.Title.Contains("Skateboard")).ToList();
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
