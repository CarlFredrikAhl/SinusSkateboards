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
    
    public class CheckoutModel : PageModel
    {
        [BindProperty]
        public Checkout Checkout { get; set; }

        private readonly AppDbContext database;

        public CheckoutModel(AppDbContext context)
        {
            database = context;
        }

        public void OnGet()
        {
            Checkout.PhoneNumber = null;
        }

        public IActionResult OnPostBuy()
        {
            //Save checkout to database
            database.Checkouts.Add(Checkout);
            database.SaveChanges();

            return RedirectToPage("/SuccessfulPurchase");
        }
    }
}
