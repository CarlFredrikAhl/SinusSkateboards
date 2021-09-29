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
    public class BoardsModel : PageModel
    {
        private readonly AppDbContext database;

        public List<ProductModel> Products { get; set; }

        public BoardsModel(AppDbContext context)
        {
            database = context;
        }

        public void OnGet()
        {
            //Add the products to the database if there are no ones already
            if (database.Products.Where(product => product.Title.Contains("Skateboard")).ToList().Count == 0)
            {
                Products = new List<ProductModel>()
                {
                    new ProductModel("sinus-skateboard-eagle.png", "Skateboard (eagle)", "Board with eagle print"
                    , "Black/White/Yellow", 80),
                    new ProductModel("sinus-skateboard-fire.png", "Skateboard (fire)", "Board with fire print"
                    , "Orange/Brown", 75),
                    new ProductModel("sinus-skateboard-gretasfury.png", "Skateboard (Gretas fury)", 
                    "Skateboard with Greta Thunberg print", "Black/Brown", 90),
                    new ProductModel("sinus-skateboard-ink.png", "Skateboard (ink)", "Board with ink print"
                    , "Wood/Black", 65),
                    new ProductModel("sinus-skateboard-logo.png", "Skateboard (logo)"
                    , "Board with only our beautiful logo", "Wood", 60),
                    new ProductModel("sinus-skateboard-northern_lights.png", "Skateboard (northen lights)"
                    , "Board with northen lights print", "Green/Blue/Black", 100),
                    new ProductModel("sinus-skateboard-plastic.png", "Skateboard (plastic)", "Board with plastic print"
                    , "Mixed plastic colors", 60),
                    new ProductModel("sinus-skateboard-polar.png", "Skateboard (polar)", "Board with polar bear print"
                    , "Black/Wood", 75),
                    new ProductModel("sinus-skateboard-purple.png", "Skateboard (purple)", "Purple board"
                    , "Purple", 95),
                    new ProductModel("sinus-skateboard-yellow.png", "Skateboard (yellow)", "Yellow board"
                    , "Yellow", 85),
                };


                foreach (var product in Products)
                {
                    database.Products.Add(product);
                }

                database.SaveChanges();
            
            }
            else
            {
                Products = database.Products.Where(product => product.Title.Contains("Skateboard")).ToList();
            }
        }
    }
}
