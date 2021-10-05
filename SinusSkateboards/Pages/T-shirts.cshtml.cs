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
    public class T_shirtsModel : PageModel
    {
        private readonly AppDbContext database;

        [BindProperty]
        public List<Product> Products { get; set; }

        [BindProperty]
        public string SearchString { get; set; }

        public int ItemsInCart { get; set; }

        public T_shirtsModel(AppDbContext context)
        {
            database = context;
        }

        public void OnGet()
        {
            //Add the products to the database if there are no ones already
            if (database.Products.Where(product => product.Title.Contains("T-shirt")).ToList().Count == 0)
            {
                Products = new List<Product>()
                {
                    new Product("sinus-tshirt-blue.png", "T-shirt (Blue)", "Blue skate t-shirt"
                    , "Blue", 15),
                    new Product("sinus-tshirt-grey.png", "T-shirt (Grey)", "Grey skate t-shirt"
                    , "Grey", 15),
                    new Product("sinus-tshirt-pink.png", "T-shirt (Pink)", "Pink skate t-shirt"
                    , "Pink", 15),
                    new Product("sinus-tshirt-purple.png", "T-shirt (Purple)", "Purple skate t-shirt"
                    , "Purple", 15),
                    new Product("sinus-tshirt-yellow.png", "T-shirt (Yellow)", "Yellow skate t-shirt"
                    , "Yellow", 15),
                };

                foreach (var product in Products)
                {
                    database.Products.Add(product);
                }

                database.SaveChanges();
            
            }
            else
            {
                Products = database.Products.Where(product => product.Title.Contains("T-shirt")).ToList();
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
            return RedirectToPage("/ChosenProduct", new { id = productId});
        }

        public IActionResult OnPostSearch()
        {
            return RedirectToPage("/Search", new { search = SearchString });
        }
    }
}
