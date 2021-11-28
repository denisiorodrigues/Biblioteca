using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace Biblioteca.API.Extensions
{
    public class ClaimsAuthorizeAttribute : TypeFilterAttribute
    {
        public ClaimsAuthorizeAttribute(string claimName, string claimValue):base(typeof(RequisitoClaimFilter))
        {
            Arguments = new object [] {new Claim(claimName, claimValue)};
        }
    }
}