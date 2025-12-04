using System.Text.Json;
using System.Xml.Linq;
using CINLAT.WebApiTest.Application.DTOs;
using CINLAT.WebApiTest.Application.Interfaces;
using CINLAT.WebApiTest.Models.Business;

namespace CINLAT.WebApiTest.Infrastructure.Services
{
    public class CountriesApi : ICountriesApi
    {
        private readonly HttpClient _httpClient;
        private const string ApiUrl = "https://restcountries.com/v3.1/all?fields=cca3";
        private const string DetailCountryUrl = "https://restcountries.com/v3.1/alpha/";
        private List<string> _countryCodes = new List<string>();

        public CountriesApi()
        {
            // Inicializa HttpClient. Es mejor usar una instancia única (singleton) en aplicaciones reales.
            _httpClient = new HttpClient();
        }
        

        /// <summary>
        /// Obtiene una lista de códigos de país de 3 letras (cca3) de la API restcountries.com.
        /// </summary>
        /// <returns>Una List<string> con los códigos CCA3.</returns>
        public async Task<List<string>> GetCountriesAsync()
        {
            
            try
            {
                // 2. Realiza la solicitud GET
                HttpResponseMessage response = await _httpClient.GetAsync(ApiUrl);

                // Asegura que la solicitud fue exitosa (código 200-299)
                response.EnsureSuccessStatusCode();

                // 3. Lee el contenido como string
                string responseBody = await response.Content.ReadAsStringAsync();

                // 4. Deserializa el JSON a una lista de objetos CountryCode
                List<CountryCode> countryObjects = JsonSerializer.Deserialize<List<CountryCode>>(
                    responseBody,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
                );

                // 5. Convierte la lista de objetos CountryCode a List<string>
                if (countryObjects != null)
                {
                    foreach (var country in countryObjects)
                    {
                        if (!string.IsNullOrEmpty(country.Cca3))
                        {
                            _countryCodes.Add(country.Cca3);
                        }
                    }
                }
            }
            catch (HttpRequestException e)
            {
                // Manejo de errores de red o HTTP
                Console.WriteLine($"Error al conectar o recibir respuesta: {e.Message}");
                // Puedes optar por relanzar la excepción o retornar una lista vacía
            }
            catch (JsonException e)
            {
                // Manejo de errores de deserialización (si el formato JSON cambia)
                Console.WriteLine($"Error al deserializar la respuesta JSON: {e.Message}");
            }
            catch (Exception e)
            {
                // Manejo de otros errores inesperados
                Console.WriteLine($"Ocurrió un error inesperado: {e.Message}");
            }

            return _countryCodes;
        }


        /// <summary>
        /// Obtiene los detalles de todos los países en la lista _countryCodes.
        /// </summary>
        /// <returns>Una lista de DTOs con la información de los países.</returns>
        public async Task<List<CountryDetailDto>> GetMultipleCountryDetailsAsync()
        {
            var allCountryDetails = new List<CountryDetailDto>();

            foreach (var code in _countryCodes)
            {
                string apiUrl = $"{DetailCountryUrl}{code}";

                try
                {
                    HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);

                    if (!response.IsSuccessStatusCode)
                    {
                        continue; // Pasa a la siguiente iteración del bucle
                    }

                    string responseBody = await response.Content.ReadAsStringAsync();

                    // La respuesta de '/alpha/{code}' es un ARRAY con un solo objeto
                    List<CountryDetailDto> countryList = JsonSerializer.Deserialize<List<CountryDetailDto>>(
                        responseBody,
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
                    );

                    // Si la deserialización fue exitosa y el array no está vacío, añade el DTO a la lista final
                    if (countryList != null && countryList.Any())
                    {
                        allCountryDetails.Add(countryList.First());
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine($"  ❌ Excepción inesperada para {code}: {e.Message}");
                }
            }

            return allCountryDetails;
        }


    }
}
