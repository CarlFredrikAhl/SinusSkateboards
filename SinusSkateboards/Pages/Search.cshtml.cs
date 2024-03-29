using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SinusSkateboards.Models;
using SinusSkateboards.Database;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace SinusSkateboards.Pages
{
    public class SearchModel : PageModel
    {
        [BindProperty]
        public string SearchString { get; set; }

        public List<Product> Products { get; set; }

        public int ItemsInCart { get; set; }

        private readonly AppDbContext database;

        public SearchModel(AppDbContext context)
        {
            database = context;
        }
        
        public void OnGet(string search)
        {
            SearchString = search;

            Products = new List<Product>();

            //Search title or color
            //Exists in database and is not bought
            Products = database.Products.Where(product => product.Title.ToUpper().Contains(search.ToUpper())
            || product.Color.ToUpper().Contains(search)).Where(product => product.OrderId == null).ToList();

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
            return RedirectToPage("/Search", new { search = SearchString});
        }
    }
}
