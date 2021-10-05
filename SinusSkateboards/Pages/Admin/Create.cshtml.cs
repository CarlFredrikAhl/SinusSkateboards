using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using SinusSkateboards.Database;
using SinusSkateboards.Models;

namespace SinusSkateboards.Pages
{
    public class CreateModel : PageModel
    {
        private readonly SinusSkateboards.Database.AppDbContext _context;
        private readonly IWebHostEnvironment webHostEnvironment; 

        public CreateModel(SinusSkateboards.Database.AppDbContext context, 
            IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            this.webHostEnvironment = webHostEnvironment;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Product Product { get; set; }

        [BindProperty]
        public IFormFile Image { get; set; } 

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (Image != null)
            {
                string folder = Path.Combine(webHostEnvironment.WebRootPath, "imgs/products");

                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }

                string ext = Path.GetExtension(Image.FileName);

                string uniqueFileName = String.Concat(Guid.NewGuid().ToString(), "-", Product.Title + ext);

                string uploadFolder = Path.Combine(folder, uniqueFileName);

                using (var fileStream = new FileStream(uploadFolder, FileMode.Create))
                {
                    Image.CopyTo(fileStream);
                }

                Product.Image = uniqueFileName;
            }

            _context.Products.Add(Product);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
