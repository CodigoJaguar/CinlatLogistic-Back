using System.Text.Json.Serialization;

namespace CINLAT.WebApiTest.Application.DTOs
{
    public class CountryCode
    {
        // 1. Clase para representar la estructura mínima de la respuesta JSON
        // Solo necesitamos el campo 'cca3'

            [JsonPropertyName("cca3")]
            public string Cca3 { get; set; }
        
    }
}
