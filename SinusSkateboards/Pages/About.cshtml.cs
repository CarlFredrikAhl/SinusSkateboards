using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using SinusSkateboards.Models;

namespace SinusSkateboards.Pages
{
    public class AboutModel : PageModel
    {
        public int ItemsInCart { get; set; }

        [BindProperty]
        public string SearchString { get; set; }

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

        //For search
        public IActionResult OnPost()
        {
            return RedirectToPage("/Search", new { search = SearchString }); 
        }
    }
}
