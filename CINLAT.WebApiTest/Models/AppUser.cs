using Microsoft.AspNetCore.Identity;

namespace CINLAT.WebApiTest.Models
{
    public class AppUser : IdentityUser
    {
        public string Password { get; set; } = null!;
        public DateTime? FechaRegistro { get; set; }
        public DateOnly? FechaCaducidad { get; set; }
        public bool? PrimeraVez { get; set; }
        public int? Bloqueo { get; set; }
        public bool? Logueado { get; set; }
        // Intentos login = AccessFailCount
        public DateTime? HoraUltimoLogin { get; set; }
        public int? TiempoBloqueoId { get; set; }
        public int? IntentosLoginBloqueo { get; set; }
        public byte[]? Salt { get; set; }
        public byte[]? Correo { get; set; }
        public string? NombreCompleto { get; set; }

    }
}
