using FluentValidation;

namespace CINLAT.WebApiTest.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(configuration =>
            {
                configuration.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);

                //PipeLine behavior
                //configuration.AddOpenBehavior(typeof(ValidationBehavior<,>));
            });

            // metodo agregado por: FluentValidation.DependencyInjectionExtensions
            services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);

            // Fix: Use the correct overload for AddAutoMapper v15
            //services.AddAutoMapper(cfg => cfg.AddMaps(typeof(MappingProfile).Assembly));

            return services;
        }
    }
}
