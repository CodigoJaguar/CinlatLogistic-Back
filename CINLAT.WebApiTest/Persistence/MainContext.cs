using CINLAT.WebApiTest.Models;
using CINLAT.WebApiTest.Models.Business;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CINLAT.WebApiTest.Persistence
{
    public class MainContext : IdentityDbContext<AppUser>
    {
        public MainContext(DbContextOptions<MainContext> options) : base(options)
        {

        }

        public DbSet<Personaje> Personajes { get; set; }
        public DbSet<Pais> Paises { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            CargarDataSeguridad(modelBuilder);

        }

        public void CargarDataSeguridad(ModelBuilder modelBuilder)
        {
            // TODO: Cambiar los GUIDs por otros nuevos en algun archivo appsettings.json
            var adminId = "5fee3086-9873-4908-b04c-7e6fa2b22c34"; // Guid.NewGuid().ToString(); ya no disponible en NET 9 por : https://github.com/dotnet/efcore/issues/35285
            var clientId = "47048c72-3ea3-4c37-803f-e3de5420f34b";

            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole
                {
                    Id = adminId,
                    Name = CustomRoles.ADMIN,
                    NormalizedName = CustomRoles.ADMIN
                });

            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole
                {
                    Id = clientId,
                    Name = CustomRoles.CLIENT,
                    NormalizedName = CustomRoles.CLIENT
                });


            modelBuilder.Entity<IdentityRoleClaim<string>>()
                .HasData(
                    new IdentityRoleClaim<string>
                    {
                        Id = 1,
                        ClaimType = CustomClaims.HEROES,
                        ClaimValue = PolicyMaster.HEROES_READ,
                        RoleId = adminId
                    },
                    new IdentityRoleClaim<string>
                    {
                        Id = 2,
                        ClaimType = CustomClaims.VILLANOS,
                        ClaimValue = PolicyMaster.VILLANOS_READ,
                        RoleId = adminId
                    },
                    new IdentityRoleClaim<string>
                    {
                        Id = 3,
                        ClaimType = CustomClaims.LISTAS,
                        ClaimValue = PolicyMaster.LISTAS_READ,
                        RoleId = adminId
                    }
 
                );

        }


    }
}
