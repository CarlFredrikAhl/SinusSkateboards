using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SinusSkateboards.Models;

namespace SinusSkateboards.Pages
{
    public class SuccessfulPurchaseModel : PageModel
    {
        public List<Product> Products { get; set; }

        public void OnGet()
        {
        }
    }
}
