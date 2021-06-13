using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using ProyectoTiendaOnline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ProyectoTiendaOnline.Security
{
    public class SecurityManager
    {
        public async void SingIn(HttpContext httpContext, Usuario usuario, string schema)
        {
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(getUserClaims(usuario), schema);
            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
            await httpContext.SignInAsync(schema, claimsPrincipal);
        }

        public async void SingOut(HttpContext httpContext, string schema)
        {
            await httpContext.SignOutAsync(schema);
        }

        //public string GetUserId(Account account, string schema)
        //{
        //    var identity = (System.Security.Claims.ClaimsPrincipal)System.Threading.Thread.CurrentPrincipal;
        //    ClaimsIdentity claimsIdentity = new ClaimsIdentity(getUserClaims(account), schema);
        //    ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
        //    var userId = identity.Claims.Where(c => c.Type == System.Security.Claims.ClaimTypes.NameIdentifier).Select(c => c.Value).SingleOrDefault();
        //    return userId;
        //}


        public static string GetUserId()
        {
            var identity = (ClaimsPrincipal)System.Threading.Thread.CurrentPrincipal;
            var principal = System.Threading.Thread.CurrentPrincipal as ClaimsPrincipal;
            var userId = identity.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).Select(c => c.Value).SingleOrDefault();
            return userId;
        }


        //public static string GetUserId(ClaimsPrincipal principal)
        //{
        //    if (principal == null)
        //        throw new ArgumentNullException(nameof(principal));

        //    return principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        //}

        //public static T GetLoggedInUserId<T>(ClaimsPrincipal principal)
        //{
        //    if (principal == null)
        //        throw new ArgumentNullException(nameof(principal));

        //    var loggedInUserId = principal.FindFirstValue(ClaimTypes.NameIdentifier);

        //    if (typeof(T) == typeof(string))
        //    {
        //        return (T)Convert.ChangeType(loggedInUserId, typeof(T));
        //    }
        //    else if (typeof(T) == typeof(int) || typeof(T) == typeof(long))
        //    {
        //        return loggedInUserId != null ? (T)Convert.ChangeType(loggedInUserId, typeof(T)) : (T)Convert.ChangeType(0, typeof(T));
        //    }
        //    else
        //    {
        //        throw new Exception("Invalid type provided");
        //    }
        //}

        private IEnumerable<Claim> getUserClaims(Usuario usuario)
        {
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, usuario.Username));
            claims.Add(new Claim(ClaimTypes.Role, usuario.Rol.Nombre));
            claims.Add(new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()));

            return claims;

        }
    }
}
