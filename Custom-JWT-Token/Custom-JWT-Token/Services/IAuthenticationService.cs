using Custom_JWT_Token.Models;

namespace Custom_JWT_Token.Services
{
    public interface IAuthenticationService
    {
        AuthenticateResponse Authenticate(AuthenticateRequest request);
    }
}
