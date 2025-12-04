using CINLAT.WebApiTest.Application.Interfaces;
using CINLAT.WebApiTest.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CINLAT.WebApiTest.Infrastructure.Security
{
    public class UserTabsPermissions : IUserTabsPermissions
    {

        private readonly MainContext _context;

        public UserTabsPermissions(MainContext context)
        {
            _context = context;
        }

        public async Task<List<string>> Permissions(string username)
        {
            var listaDeRutas = new List<string>();
            if (string.IsNullOrEmpty(username)) return listaDeRutas;


            var policies = await _context.Database.SqlQuery<string>($@"
                SELECT 
                    DISTINCT aspr.ClaimType
                FROM AspNetUsers a
                LEFT JOIN AspNetUserRoles ar
                ON a.Id = ar.UserId
                LEFT JOIN AspNetRoleClaims aspr
                ON ar.RoleId = aspr.RoleId
                WHERE a.Username = {username}
            ").ToListAsync();

            //listaDeRutas = Routes.GetRoutes(policies);
            if (policies.Count > 0) return listaDeRutas;

            return policies;
        }

        public async Task<List<string>> GetSectionPermissions(string username)
        {
            var listaDeSecciones = new List<string>();
            if (string.IsNullOrEmpty(username)) return listaDeSecciones;

            var policies = await _context.Database.SqlQuery<string>($@"
                SELECT 
                    DISTINCT aspr.ClaimType
                FROM AspNetUsers a
                LEFT JOIN AspNetUserRoles ar
                ON a.Id = ar.UserId
                LEFT JOIN AspNetRoleClaims aspr
                ON ar.RoleId = aspr.RoleId
                WHERE a.Username = {username}
            ").ToListAsync();

            foreach (string element in policies)
            {
                listaDeSecciones.Add(element);
            }

            return listaDeSecciones;
        }

    }
}
