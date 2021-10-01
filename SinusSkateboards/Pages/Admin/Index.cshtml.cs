using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SinusSkateboards.Pages.Admin
{
	public class IndexModel : PageModel
	{
		private readonly SignInManager<IdentityUser> signInManager;

		public IndexModel(SignInManager<IdentityUser> signInManager)
		{
			this.signInManager = signInManager;
		}

		public void OnGet()
		{

		}

		public IActionResult OnPost()
		{
			signInManager.SignOutAsync();

			return RedirectToPage("/Index");
		}
	}
}
