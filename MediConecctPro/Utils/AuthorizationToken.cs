using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MediConecctPro.Utils
{
    public class AuthorizationToken : IAuthorizationFilter
    {
        private readonly IConfiguration _config;
        public AuthorizationToken(IConfiguration config)
        {
            _config = config;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var hasAuthorizeAttribute = context.ActionDescriptor.EndpointMetadata
                .Any(em => em is AuthorizeAttribute);

            if (hasAuthorizeAttribute)
            {
                var token = context.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

                if (!string.IsNullOrEmpty(token))
                {
                    try
                    {
                        var claimsPrincipal = ValidateToken(token);

                        var userId = claimsPrincipal.FindFirst(ClaimTypes.Authentication)?.Value;
                        context.HttpContext.Session.SetString("KeyUser", userId!);
                    }
                    catch (Exception)
                    {
                        context.Result = new UnauthorizedResult();
                    }
                }
                else
                {
                    string? controller = context.RouteData.Values.Values.ToList()[1]!.ToString()!.Trim();
                    string? url = context.RouteData.Values.Values.First()!.ToString();
                    if (url!.ToString()!.Trim() == "Autentication" || controller!.ToString() == "Generics")
                    {
                        return;
                    }
                    context.Result = new UnauthorizedResult();
                }
            }
        }

        private ClaimsPrincipal ValidateToken(string token)
        {
            var key = Encoding.ASCII.GetBytes(_config["Jwt:Key"]!);
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var claimsPrincipal = tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = _config["Jwt:Issuer"]!,
                    ValidAudience = _config["Jwt:Audience"]!,
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                }, out SecurityToken validatedToken);
                return claimsPrincipal;
            }
            catch (SecurityTokenExpiredException)
            {
                throw new ApplicationException("Token a expirado.");
            }
        }

    }
}
