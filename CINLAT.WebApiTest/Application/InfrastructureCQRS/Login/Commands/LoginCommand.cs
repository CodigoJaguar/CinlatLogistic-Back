using CINLAT.WebApiTest.Application.Core;
using CINLAT.WebApiTest.Application.DTOs;
using MediatR;

namespace CINLAT.WebApiTest.Application.Infrastructure.Login.Commands
{
    public class LoginCommand
    {
        public record LoginCommandRequest(LoginRequest loginRequest) : IRequest<Result<Profile>>;

    }
}
