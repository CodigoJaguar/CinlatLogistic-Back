using System.ComponentModel.DataAnnotations;

namespace CINLAT.WebApiTest.Models.Business
{
    public class Pais
    {
        [Key]
        public required string cca3 { get; set; }
        public string commonName { get; set; } = "No common name"; // name.common
        public string officialName { get; set; } = "No official name"; // name.official
        public bool independent { get; set; }
        public double area { get; set; }
        public long population { get; set; }
        public string? flag { get; set; } // flags.png

    }
}
