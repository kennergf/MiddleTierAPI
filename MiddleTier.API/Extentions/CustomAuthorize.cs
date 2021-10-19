using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MiddleTier.Api.Extensions
{
    public class CustomAuthorization
    {
        /// <summary>
        /// Validate User Claims against required Claims to access the method
        /// </summary>
        /// <param name="context"></param>
        /// <param name="claimName"></param>
        /// <param name="claimValue"></param>
        /// <returns></returns>
        public static bool ValidateUserClaims(HttpContext context, string claimName, string claimValue)
        {
            return context.User.Identity.IsAuthenticated &&
                   context.User.Claims.Any(c => c.Type == claimName && c.Value.Contains(claimValue));
        }
    }

    /// <summary>
    /// Authorize User access based on Claims
    /// </summary>
    public class ClaimsAuthorizeAttribute : TypeFilterAttribute
    {
        public ClaimsAuthorizeAttribute(string claimName, string claimValue) : base(typeof(AuthorizationClaimFilter))
        {
            Arguments = new object[] { new Claim(claimName, claimValue) };
        }
    }

    public class AuthorizationClaimFilter : IAuthorizationFilter
    {
        private readonly Claim _claim;

        public AuthorizationClaimFilter(Claim claim)
        {
            _claim = claim;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            // If User Is not Authenticated
            if (!context.HttpContext.User.Identity.IsAuthenticated)
            {
                context.Result = new StatusCodeResult(401);
                return;
            }

            // If User Claims does not match Method Claims
            if (!CustomAuthorization.ValidateUserClaims(context.HttpContext, _claim.Type, _claim.Value))
            {
                context.Result = new StatusCodeResult(403);
            }
        }
    }
}