using CINLAT.WebApiTest.Application.Core;
using CINLAT.WebApiTest.Application.DTOs;
using CINLAT.WebApiTest.Application.Infrastructure.Login;
using CINLAT.WebApiTest.Application.Interfaces;
using CINLAT.WebApiTest.Models;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using static CINLAT.WebApiTest.Application.Infrastructure.Login.Commands.LoginCommand;

namespace CINLAT.WebApiTest.Application.InfrastructureCQRS.Login.Handlers
{
    public class LoginCommandHandler : IRequestHandler<LoginCommandRequest, Result<Profile>>
    {
  

            private readonly UserManager<AppUser> _userManager;

            private readonly ITokenService _tokenService;
            private readonly IUserTabsPermissions _TabsPermission;

            public LoginCommandHandler(UserManager<AppUser> userManager, ITokenService tokenService, IUserTabsPermissions TabsPermission)
            {
                _userManager = userManager;
                _tokenService = tokenService;
                _TabsPermission = TabsPermission;
            }

            public async Task<Result<Profile>> Handle(LoginCommandRequest request, CancellationToken cancellationToken)
            {
                var user = await _userManager.Users
                                 .FirstOrDefaultAsync(x => x.Email == request.loginRequest.Email, cancellationToken);


                if (user is null)
                {
                    return Result<Profile>.Failure("El usuario no existe");
                }

                var result = await _userManager.CheckPasswordAsync(user, request.loginRequest.Password!);

                if (!result)
                {
                    return Result<Profile>.Failure("Contraseña incorrecta");
                }

                var profile = new Profile
                {
                    Email = user.Email,
                    NombreCompleto = user.NombreCompleto,
                    Username = user.UserName,
                    Token = await _tokenService.CreateToken(user),
                    Rutas = await _TabsPermission.Permissions(user.UserName),
                    Secciones = await _TabsPermission.GetSectionPermissions(user.UserName)
                };

                return Result<Profile>.Success(profile);

            }

            public class LoginRequestCommandValidator : AbstractValidator<LoginCommandRequest>
            {
                public LoginRequestCommandValidator()
                {
                    RuleFor(x => x.loginRequest).SetValidator(new LoginValidator());
                }
            }

        

    }
}
