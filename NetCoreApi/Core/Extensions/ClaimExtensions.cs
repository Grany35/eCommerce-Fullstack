using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Core.Extensions
{
    public static class ClaimExtensions
    {
        public static void AddNameIdentitfier(this ICollection<Claim> claims, string nameIdentitfier)
        {
            claims.Add(new Claim(ClaimTypes.NameIdentifier, nameIdentitfier));
        }

        public static void AddRole(this ICollection<Claim> claims, string[] roles)
        {
            roles.ToList().ForEach(role => claims.Add(new Claim(ClaimTypes.Role, role)));
        }
    }
}
