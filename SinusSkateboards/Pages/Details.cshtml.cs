﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SinusSkateboards.Database;
using SinusSkateboards.Models;

namespace SinusSkateboards.Pages
{
    public class DetailsModel : PageModel
    {
        private readonly SinusSkateboards.Database.AppDbContext _context;

        public DetailsModel(SinusSkateboards.Database.AppDbContext context)
        {
            _context = context;
        }

        public Product Product { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Product = await _context.Products.FirstOrDefaultAsync(m => m.ProductId == id);

            if (Product == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
