using CINLAT.WebApiTest.Application.Interfaces;
using CINLAT.WebApiTest.Models;
using CINLAT.WebApiTest.Persistence;
using Microsoft.AspNetCore.Identity;

namespace CINLAT.WebApiTest.Infrastructure.Security
{
    public static class IdentityServices
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddIdentityCore<AppUser>(options =>
            {
                options.Password.RequireNonAlphanumeric = false;
            }).AddRoles<IdentityRole>().AddEntityFrameworkStores<MainContext>();

            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IUserAccessor, UserAccessor>();
            services.AddScoped<IUserTabsPermissions, UserTabsPermissions>();

            return services;
        }
    }
}
