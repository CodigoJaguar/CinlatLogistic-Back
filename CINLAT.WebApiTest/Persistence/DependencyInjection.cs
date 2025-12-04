using Microsoft.EntityFrameworkCore;

namespace CINLAT.WebApiTest.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            //  Migracion con base de datos SQlite
            services.AddDbContext<MainContext>(options =>
            {
                options.LogTo(Console.WriteLine, new[] {
                DbLoggerCategory.Database.Command.Name
            }, LogLevel.Information).EnableSensitiveDataLogging();
                options.UseSqlite(configuration.GetConnectionString("SqliDatabase"));
            });



            // Migracion con base de datos SQL server
            //services.AddDbContext<MainContext>(options =>
            //{
            //    var builder = options.UseSqlServer(configuration.GetConnectionString("LocalConnection"));
            //});


            return services;
        }
    }
}
