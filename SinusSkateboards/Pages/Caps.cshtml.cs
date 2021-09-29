using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SinusSkateboards.Database;
using SinusSkateboards.Models;

namespace SinusSkateboards.Pages
{
    public class CapsModel : PageModel
    {
        private readonly AppDbContext database;

        public List<ProductModel> Products { get; set; }

        public CapsModel(AppDbContext context)
        {
            database = context;
        }

        public void OnGet()
        {
            //Add the products to the database if there are no ones already
            if (!database.Products.Any())
            {
                Products = new List<ProductModel>()
                {
                    new ProductModel("sinus-cap-blue.png", "Cap (blue)", "Blue skate cap"
                    , "Blue", 10),
                    new ProductModel("sinus-cap-green.png", "Cap (green)", "Green skate cap"
                    , "Green", 10),
                    new ProductModel("sinus-cap-purple.png", "Cap (purple)", "Purple skate cap"
                    , "Purple", 10),
                    new ProductModel("sinus-cap-red.png", "Cap (red)", "Red skate cap"
                    , "Red", 10),
                };

                //foreach (var product in Products)
                //{
                //    database.Products.Add(product);
                //}

                //database.SaveChanges();
            }
        }
    }
}
