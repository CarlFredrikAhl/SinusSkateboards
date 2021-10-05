using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SinusSkateboards.Database;
using System;
using System.Linq;

namespace SinusSkateboards
{
    public class Program
    {
        public static void Main(string[] args)
        {
			var host = CreateHostBuilder(args).Build();

			//Create admin user
			try
			{
				using (var scope = host.Services.CreateScope())
				{
					var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
					var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

					context.Database.Migrate();

					if (!context.Users.Any())
					{
						var adminUser = new IdentityUser()
						{
							UserName = "admin"
						};

						var result = userManager.CreateAsync(adminUser, "password").GetAwaiter().GetResult();
					}
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}

			host.Run();
		}

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
