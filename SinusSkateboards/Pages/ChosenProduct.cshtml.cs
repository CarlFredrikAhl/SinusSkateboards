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
    public class ChosenProductModel : PageModel
    {
        private readonly AppDbContext database;

        string productType;

        public Product Product { get; set; }
        public List<Product> AlsoInThisColorProducts { get; set; }

        public int ItemsInCart { get; set; }

        public ChosenProductModel(AppDbContext context)
        {
            database = context;
            productType = String.Empty;
        }

        public void OnGet(int id)
        {

            Product = new Product();
            AlsoInThisColorProducts = new List<Product>();
            Product = database.Products.Where(product => product.ProductId == id).FirstOrDefault();

            productType = Product.Title.Split(' ')[0];

            AlsoInThisColorProducts = database.Products.Where(product => product.Title.Contains(productType)
            && product.Color != Product.Color).ToList();


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

            return RedirectToPage("/ChosenProduct", new { id = productId});
        }
    }
}
