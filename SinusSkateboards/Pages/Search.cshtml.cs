using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SinusSkateboards.Pages
{
    public class SearchModel : PageModel
    {
        [BindProperty]
        public string SearchString { get; set; }
        
        public void OnGet(string searchString)
        {

        }
    }
}
