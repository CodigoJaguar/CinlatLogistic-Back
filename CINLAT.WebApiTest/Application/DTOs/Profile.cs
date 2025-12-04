namespace CINLAT.WebApiTest.Application.DTOs
{
    public class Profile
    {
        public string? NombreCompleto { get; set; }
        public string? Email { get; set; }
        public string? Token { get; set; }
        public string? Username { get; set; }
        public List<string>? Rutas { get; set; }
        public List<string>? Secciones { get; set; }
    }
}
