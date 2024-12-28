using Custom_JWT_Token.Services;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Custom_JWT_Token.Helpers
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;

        private readonly AppSettings _appSettings;

        public JwtMiddleware(RequestDelegate next, IOptions<AppSettings> appSettings)
        {
            this._next = next;
            this._appSettings = appSettings.Value;
        }

        public async Task Invoke(HttpContext context, IUserService userService)
        {
            var token= context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            if (token != null)
            {
                attachUserToContext(context, userService, token);
            }
            _next(context);
        }

        private void attachUserToContext(HttpContext context, IUserService userService, string token) 
        {
            try
            {
                var JwtTokenHandler = new JwtSecurityTokenHandler();
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.Key));

                JwtTokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = key,
                    ClockSkew = TimeSpan.Zero,
                    ValidAudience = _appSettings.Issuer,
                    ValidIssuer = _appSettings.Issuer,
                }, out SecurityToken validatedToken);
                var jwtToken = (JwtSecurityToken)validatedToken;
                var userId = jwtToken.Claims.FirstOrDefault(x => x.Type == "Id").Value;
                context.Items["User"] = userService.GetById(userId);
            }
            catch (Exception ex)
            {

            }
        }
    }
}
