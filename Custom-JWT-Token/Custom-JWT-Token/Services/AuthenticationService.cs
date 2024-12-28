using Custom_JWT_Token.Helpers;
using Custom_JWT_Token.Models;
using Custom_JWT_Token.Repository;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Custom_JWT_Token.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        //private List<User> _users = new List<User> {
        //    new User {
        //        Id = "1", 
        //        FirstName = "mytest", 
        //        LastName = "User", 
        //        UserName= "mytestuser",
        //        userRole= new List<Role>{Role.Customer} , 
        //        Password = "test123"
        //    }
        //};
        private readonly AppSettings _appSettings;

        private readonly IUserRepository _userRepository;

        public AuthenticationService(IOptions<AppSettings> appSessings, IUserRepository _userRepository)
        {
            this._appSettings = appSessings.Value;
            this._userRepository = _userRepository;
        }

        public AuthenticateResponse Authenticate(AuthenticateRequest request)
        {
            AuthenticateResponse response = new AuthenticateResponse();
            User foundUser = _userRepository.AuthenticateUser(new User { UserName = request.UserName, Password = request.Password });
            if (foundUser!=null)
            {
               var generatedJWTToken= GenerateJWTToken(foundUser);
               response.Token = generatedJWTToken;
            }
            return  response;
        }

        private string GenerateJWTToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.Key));
            var credentials = new SigningCredentials(securityKey,SecurityAlgorithms.HmacSha256);

            List<Claim> claims = new List<Claim>(){
                    new Claim("Id",Convert.ToString(user.Id)),
                    new Claim(JwtRegisteredClaimNames.Sub, "Test"),
                    new Claim(JwtRegisteredClaimNames.Email, "test@gmail.com"),
                    //new Claim("Role", Convert.ToString(user.Role)),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())

            };

            foreach(var role in user.userRole)
            {
                claims.Add(new Claim("Role", Convert.ToString(role)));
            }

            var token = new JwtSecurityToken(_appSettings.Issuer, _appSettings.Issuer, claims, expires: DateTime.UtcNow.AddMinutes(10), signingCredentials:credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
