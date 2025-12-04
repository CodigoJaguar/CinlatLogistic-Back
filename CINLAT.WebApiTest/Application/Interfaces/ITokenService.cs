using CINLAT.WebApiTest.Models;

namespace CINLAT.WebApiTest.Application.Interfaces
{
    public interface ITokenService
    {
        Task<string> CreateToken(AppUser userId);
    }
}
