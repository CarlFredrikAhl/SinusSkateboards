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
    public class WheelsModel : PageModel
    {
        private readonly AppDbContext database;

        public List<ProductModel> Products { get; set; }

        public WheelsModel(AppDbContext context)
        {
            database = context;
        }

        public void OnGet()
        {
            //Add the products to the database if there are no ones already
            if (database.Products.Where(product => product.Title.Contains("Wheel")).ToList().Count == 0)
            {
                Products = new List<ProductModel>()
                {
                    new ProductModel("sinus-wheel-rocket.png", "Wheel (rocket)", "Four rocket skate wheels"
                    , "White/Red", 20),
                    new ProductModel("sinus-wheel-spinner.png", "Wheel (spinner)", "Four white skate wheels"
                    , "White", 20),
                    new ProductModel("sinus-wheel-wave.png", "Wheel (wave)", "Four wave skate wheels"
                    , "White/Black", 20),
                };

                foreach (var product in Products)
                {
                    database.Products.Add(product);
                }

                database.SaveChanges();
            
            }
            else
            {
                Products = database.Products.Where(product => product.Title.Contains("Wheel")).ToList();
            }
        }
    }
}
