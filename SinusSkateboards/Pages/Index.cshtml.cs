using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SinusSkateboards.Database;
using SinusSkateboards.Models;

namespace SinusSkateboards.Pages
{
    [BindProperties]
    public class IndexModel : PageModel
    {
        private readonly AppDbContext database;

        public List<ProductModel> Products { get; set; }

        public IndexModel(AppDbContext context)
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
                    new ProductModel(),
                    new ProductModel(),
                    new ProductModel(),
                    new ProductModel(),
                    new ProductModel(),
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
