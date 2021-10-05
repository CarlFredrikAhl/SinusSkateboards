using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using SinusSkateboards.Models;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;

namespace SinusSkateboards.Pages
{
    [BindProperties]
    public class IndexModel : PageModel
    {
        public string SearchString { get; set; }

        public int ItemsInCart { get; set; }

        public void OnGet()
        {
            //Check how many items in cart (doesn't display right if you press back button, fix this later if I have time)
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

        public IActionResult OnPost()
        {
            return RedirectToPage("/Search", new { search = SearchString});
        }
    }
}
