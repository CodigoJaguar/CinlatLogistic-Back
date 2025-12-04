using CINLAT.WebApiTest.Models;
using CINLAT.WebApiTest.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CINLAT.WebApiTest.Infrastructure.Extensions
{
    public static class DataSeed
    {
        public static async Task SeedDataAutentication(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var service = scope.ServiceProvider;
            var loggerFactory = service.GetRequiredService<ILoggerFactory>();

            try
            {
                var context = service.GetRequiredService<MainContext>();
                await context.Database.MigrateAsync();


                var userManager = service.GetRequiredService<UserManager<AppUser>>();

                if (!userManager.Users.Any())
                {
                    var userAdmin = new AppUser
                    {
                        NombreCompleto = "Santander Mendoza",
                        UserName = "SantaMz",
                        Email = "Santander_mz@gmail.com",
                        Password = "1"
                    };

                    await userManager.CreateAsync(userAdmin, "Password123$");
                    await userManager.AddToRoleAsync(userAdmin, CustomRoles.ADMIN);
                    // otro user
                    var userClient = new AppUser
                    {
                        NombreCompleto = "Cinlat Logistic",
                        UserName = "Cinlat",
                        Email = "cinlatlogistic@gmail.com",
                        Password = "1"
                    };

                    await userManager.CreateAsync(userClient, "Password123$");
                    await userManager.AddToRoleAsync(userClient, CustomRoles.CLIENT);
                }


                await context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                var logger = loggerFactory.CreateLogger<MainContext>();
                logger.LogError(e.Message);
            }
        }

    }
}
