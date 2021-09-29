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
    public class HoodiesModel : PageModel
    {
        private readonly AppDbContext database;

        public List<ProductModel> Products { get; set; }

        public HoodiesModel(AppDbContext context)
        {
            database = context;
        }

        public void OnGet()
        {
            //Add the products to the database if there are no ones already
            if (database.Products.Where(product => product.Title.Contains("Hoodie")).ToList().Count == 0)
            {
                Products = new List<ProductModel>()
                {
                    new ProductModel("hoodie-ash.png", "Hoodie (ash)", "Grey skate hoodie"
                    , "Grey", 35),
                    new ProductModel("hoodie-fire.png", "Hoodie (fire)", "Red skate hoodie"
                    , "Red", 35),
                    new ProductModel("hoodie-green.png", "Hoodie (green)", "Green skate hoodie"
                    , "Green", 35),
                    new ProductModel("hoodie-ocean.png", "Hoodie (ocean)", "Blue skate hoodie"
                    , "Blue", 35),
                    new ProductModel("hoodie-purple.png", "Hoodie (purple)", "Purple skate hoodie"
                    , "Purple", 35),
                };

                foreach (var product in Products)
                {
                    database.Products.Add(product);
                }

                database.SaveChanges();
            
            } else
            {
                Products = database.Products.Where(product => product.Title.Contains("Hoodie")).ToList();
            }
        }
    }
}
