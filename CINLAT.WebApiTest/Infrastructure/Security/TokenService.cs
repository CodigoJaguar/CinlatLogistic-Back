using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CINLAT.WebApiTest.Application.Interfaces;
using CINLAT.WebApiTest.Models;
using CINLAT.WebApiTest.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace CINLAT.WebApiTest.Infrastructure.Security
{
    public class TokenService : ITokenService
    {
        private readonly MainContext _context;
        private readonly IConfiguration _configuration;

        public TokenService(MainContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<string> CreateToken(AppUser user)
        {
            // Leer las políticas asociadas al usuario desde la base de datos
            var policies = await _context.Database.SqlQuery<string>($@"
                SELECT 
                    aspr.ClaimValue
                FROM AspNetUsers a
                LEFT JOIN AspNetUserRoles ar
                ON a.Id = ar.UserId
                LEFT JOIN AspNetRoleClaims aspr
                ON ar.RoleId = aspr.RoleId
                WHERE a.Id = {user.Id}
            ").ToListAsync();


            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.UserName!),
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Email, user.Email!)
        };

            if (policies[0] != null)
            {
                foreach (var policy in policies)
                {
                    claims.Add(new Claim(CustomClaims.VILLANOS, policy));
                    claims.Add(new Claim(CustomClaims.HEROES, policy));
                }
            }
            


            var creds = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["TokenKey"]!)),
                SecurityAlgorithms.HmacSha256
            );


            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };


            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

    }
}
