using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using SinusSkateboards.Database;
using SinusSkateboards.Models;

namespace SinusSkateboards.Pages
{
    public class CapsModel : PageModel
    {
        private readonly AppDbContext database;

        public List<Product> Products { get; set; }

        public int ItemsInCart { get; set; }

        [BindProperty]
        public string SearchString { get; set; }

        public CapsModel(AppDbContext context)
        {
            database = context;
        }

        public void OnGet()
        {
            //Add the products to the database if there are no ones already
            if (database.Products.Where(product => product.Title.Contains("Cap")).ToList().Count == 0)
            {
                Products = new List<Product>()
                {
                    new Product("sinus-cap-blue.png", "Cap (Blue)", "Blue skate cap"
                    , "Blue", 10),
                    new Product("sinus-cap-green.png", "Cap (Green)", "Green skate cap"
                    , "Green", 10),
                    new Product("sinus-cap-purple.png", "Cap (Purple)", "Purple skate cap"
                    , "Purple", 10),
                    new Product("sinus-cap-red.png", "Cap (Red)", "Red skate cap"
                    , "Red", 10),
                };

                foreach (var product in Products)
                {
                    database.Products.Add(product);
                }

                database.SaveChanges();
            
            }
            else
            {
                //Exist in database and is not bought
                Products = database.Products.Where(product => product.Title.Contains("Cap") && product.OrderId == null).ToList();
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
