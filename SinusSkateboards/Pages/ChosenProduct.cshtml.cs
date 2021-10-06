using System;
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
    public class ChosenProductModel : PageModel
    {
        private readonly AppDbContext database;

        public string ProductType { get; set; }

        public Product Product { get; set; }
        public List<Product> AlsoInThisColorProducts { get; set; }

        [BindProperty]
        public string SearchString { get; set; }

        public int ItemsInCart { get; set; }

        public ChosenProductModel(AppDbContext context)
        {
            database = context;
            ProductType = String.Empty;
        }

        public void OnGet(int id)
        {

            Product = new Product();
            AlsoInThisColorProducts = new List<Product>();
            Product = database.Products.Where(product => product.ProductId == id).FirstOrDefault();

            ProductType = Product.Title.Split(' ')[0];

            AlsoInThisColorProducts = database.Products.Where(product => product.Title.Contains(ProductType)
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

        //Go to product
        public IActionResult OnPost(int productId)
        {
            return RedirectToPage("/ChosenProduct", new { id = productId });
        }

        //Add to cart
        public IActionResult OnPostAddToCart(int productId)
        {
            //Clicked product
            var product = database.Products.Where(product => product.ProductId == productId && product.OrderId == null).FirstOrDefault();

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

        public IActionResult OnPostSearch()
        {
            return RedirectToPage("/Search", new { search = SearchString });
        }
    }
}
