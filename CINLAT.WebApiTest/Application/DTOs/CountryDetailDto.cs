using System.Text.Json.Serialization;

namespace CINLAT.WebApiTest.Application.DTOs
{
    public class CountryDetailDto
    {
        [JsonPropertyName("name")]
        public NameDto Name { get; set; }

        [JsonPropertyName("independent")]
        public bool independent { get; set; }

        [JsonPropertyName("population")]
        public long population { get; set; }

        [JsonPropertyName("area")]
        public double area { get; set; }

        [JsonPropertyName("flags")]
        public Flag flag { get; set; }

        // El campo 'cca3' ya viene incluido en la respuesta, lo capturamos
        [JsonPropertyName("cca3")]
        public string cca3 { get; set; }
    }


    public class NameDto
    {
        // Mapea el campo JSON "common" a la propiedad Common
        [JsonPropertyName("common")]
        public string Common { get; set; }

        [JsonPropertyName("official")]
        public string Official { get; set; }
    }

    public class Flag
    {
        // Mapea el campo JSON "common" a la propiedad Common
        [JsonPropertyName("png")]
        public string png { get; set; }

        [JsonPropertyName("svg")]
        public string svg { get; set; }
    }
}
