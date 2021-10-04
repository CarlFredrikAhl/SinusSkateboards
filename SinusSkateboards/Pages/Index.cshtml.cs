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
        public string SearchString { get; set; }

        public void OnGet()
        {
        }

        //Go to the search page with searchString data
        public IActionResult OnPost()
        {
            return RedirectToPage("/Search", new { search = SearchString});
        }
    }
}
