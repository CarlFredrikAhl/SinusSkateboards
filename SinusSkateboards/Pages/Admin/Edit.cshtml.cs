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
using Microsoft.EntityFrameworkCore;
using SinusSkateboards.Database;
using SinusSkateboards.Models;

namespace SinusSkateboards.Pages
{
    public class EditModel : PageModel
    {
        private readonly SinusSkateboards.Database.AppDbContext database;
        private readonly IWebHostEnvironment webHostEnvironment;

        public EditModel(SinusSkateboards.Database.AppDbContext context,
            IWebHostEnvironment webHostEnvironment)
        {
            database = context;
            this.webHostEnvironment = webHostEnvironment;
        }

        [BindProperty]
        public Product Product { get; set; }

        [BindProperty]
        public IFormFile Image { get; set; } 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Product = await database.Products.FirstOrDefaultAsync(m => m.ProductId == id);

            if (Product == null)
            {
                return NotFound();
            }
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
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
            
            } else
            {
                Product.Image = database.Products.Where(product => product.ProductId == Product.ProductId && product.OrderId == null)
                    .Select(product => product.Image).FirstOrDefault();
            }

            database.Attach(Product).State = EntityState.Modified;

            try
            {
                await database.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(Product.ProductId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("/Admin/Crud");
        }

        private bool ProductExists(int id)
        {
            return database.Products.Any(e => e.ProductId == id);
        }
    }
}
