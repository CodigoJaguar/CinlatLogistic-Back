using CINLAT.WebApiTest.Application.DTOs;
using CINLAT.WebApiTest.Infrastructure.Services;
using CINLAT.WebApiTest.Models.Business;

namespace CINLAT.WebApiTest.Persistence.LoadData
{
    public class CountryLoadService
    {
        private readonly MainContext _context;

        public CountryLoadService(MainContext context)
        {
            _context = context;
        }


        public async Task LoadCountriesAsync()
        {

            var countriesApi = new CountriesApi();
            var countries = await countriesApi.GetCountriesAsync();
            var countryDtos = await countriesApi.GetMultipleCountryDetailsAsync();

            foreach (var country in countries)
            {
                Console.WriteLine(country);
            }


            // Crea la base de datos y aplica la estructura si no existe
            using (var context = _context)
            {
                await context.Database.EnsureCreatedAsync();

                // Mapeo e inserción de registros
                foreach (var dto in countryDtos)
                {
                    var existingPais = await context.Paises.FindAsync(dto.cca3);

                    if (existingPais == null)
                    {
                        var pais = new Pais
                        {
                            cca3 = dto.cca3,
                            commonName = dto.Name?.Common ?? "N/A",
                            officialName = dto.Name?.Official ?? "N/A",
                            independent = dto.independent,
                            area = dto.area,
                            population = dto.population,
                            flag = dto.flag?.png
                        };


                        // Usaremos Add, si hay duplicados, EF Core lanzará una excepción 
                        context.Paises.Add(pais);

                    }
                }

                // 3. Guardar todos los cambios en la base de datos en una única transacción
                int changes = await context.SaveChangesAsync();
                Console.WriteLine($"\n✅ Guardado completado. Se insertaron {changes} registros.");
            }
        }
    }
}
