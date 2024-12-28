using Custom_JWT_Token.Models;
using Custom_JWT_Token.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Custom_JWT_Token.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        public AuthenticationController(IAuthenticationService authenticationService)
        {
            this._authenticationService = authenticationService;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("Login")]
        public IActionResult Login(AuthenticateRequest requestData)
        {
            var response = _authenticationService.Authenticate(requestData);
            if(response.Token!=null)
            {
                return new JsonResult(new {token=response.Token});
            }
            else
            {
                return new JsonResult(new { message = "UnAuthorized", StatusCodes.Status401Unauthorized });
            }
        }
    }
}
