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
    public class T_shirtsModel : PageModel
    {
        private readonly AppDbContext database;

        public List<ProductModel> Products { get; set; }

        public T_shirtsModel(AppDbContext context)
        {
            database = context;
        }

        public void OnGet()
        {
            //Add the products to the database if there are no ones already
            if (database.Products.Where(product => product.Title.Contains("T-shirt")).ToList().Count == 0)
            {
                Products = new List<ProductModel>()
                {
                    new ProductModel("sinus-tshirt-blue.png", "T-shirt (blue)", "Blue skate t-shirt"
                    , "Blue", 15),
                    new ProductModel("sinus-tshirt-grey.png", "T-shirt (grey)", "Grey skate t-shirt"
                    , "Blue", 15),
                    new ProductModel("sinus-tshirt-pink.png", "T-shirt (pink)", "Pink skate t-shirt"
                    , "Blue", 15),
                    new ProductModel("sinus-tshirt-purple.png", "T-shirt (purple)", "Purple skate t-shirt"
                    , "Blue", 15),
                    new ProductModel("sinus-tshirt-yellow.png", "T-shirt (yellow)", "Yellow skate t-shirt"
                    , "Blue", 15),
                };

                foreach (var product in Products)
                {
                    database.Products.Add(product);
                }

                database.SaveChanges();
            
            }
            else
            {
                Products = database.Products.Where(product => product.Title.Contains("T-shirt")).ToList();
            }
        }
    }
}
