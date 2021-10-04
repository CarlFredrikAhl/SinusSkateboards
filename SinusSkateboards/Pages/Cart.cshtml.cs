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
    public class CartModel : PageModel
    {
        private readonly AppDbContext database;

        public Cart Cart { get; set; }

        public List<int> SameProductId { get; set; }

        public List<Product> DistinctProducts { get; set; }

        public List<string> DistinctTitles { get; set; }

        public Dictionary<int, int> ProductIdCount { get; set; }

        public CartModel(AppDbContext context)
        {
            database = context;
        }

        public void OnGet()
        {
            Cart = new Cart();

            ProductIdCount = new Dictionary<int, int>();

            //SameProductId = new List<int>();
            DistinctProducts = new List<Product>();
            DistinctTitles = new List<string>();

            List<Product> cookieProducts = new List<Product>();

            string stringProducts = HttpContext.Session.GetString("cart_items");

            //Cookie products exists in the cart already
            if (stringProducts != null)
            {
                cookieProducts = JsonConvert.DeserializeObject<List<Product>>(stringProducts);
            }

            Cart.Products = cookieProducts.ToList();

            List<string> allTitles = new List<string>();

            foreach (var product in Cart.Products)
            {
                allTitles.Add(product.Title);
            }

            DistinctTitles = allTitles.Distinct().ToList();

            foreach(var title in DistinctTitles)
            {
                DistinctProducts.Add(database.Products.Where(product => product.Title == title).FirstOrDefault());
            }

            //Algorithm to know the quantity of all products
            foreach (var product in Cart.Products)
            {
                int count;

                if(ProductIdCount.ContainsKey(product.ProductId))
                {
                    count = ProductIdCount[product.ProductId];
                    
                } else
                {
                    count = 0;
                }

                foreach (var title in DistinctTitles)
                {
                    if(product.Title == title)
                    {
                        count++;
                        
                        if(!ProductIdCount.ContainsKey(product.ProductId))
                        {
                            ProductIdCount.Add(product.ProductId, count);
                        
                        } else
                        {
                            ProductIdCount[product.ProductId] = count;
                        }
                    }
                }
            }
        }

        //Metod for deleating product from cart
        public IActionResult OnPostDelete(int productId)
        {
            List<Product> cookieProducts = new List<Product>();

            string stringProducts = HttpContext.Session.GetString("cart_items");

            //Cookie products exists in the cart already
            if (stringProducts != null)
            {
                cookieProducts = JsonConvert.DeserializeObject<List<Product>>(stringProducts);
            }

            Product productToRemove = cookieProducts.Where(product => product.ProductId == productId).FirstOrDefault();

            cookieProducts.Remove(productToRemove);

            stringProducts = JsonConvert.SerializeObject(cookieProducts);

            HttpContext.Session.SetString("cart_items", stringProducts);

            if(cookieProducts.Any())
            {
                return RedirectToPage("/Cart");
            
            } else
            {
                //Try to change this to the previous page if I have time
                return RedirectToPage("/Index");
            }
        }
    }
}

