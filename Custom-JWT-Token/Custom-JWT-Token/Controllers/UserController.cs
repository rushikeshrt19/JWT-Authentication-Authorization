using Custom_JWT_Token.Helpers;
using Custom_JWT_Token.Models;
using Custom_JWT_Token.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Custom_JWT_Token.Controllers
{
  
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService _userService)
        {
            this._userService = _userService;
        }

        [Authorization(Role.Admin)]
        [HttpPost]
        [Route("AddUser")]
        public IActionResult AddUser(User user)
        {
            var response = _userService.AddNewUser(user);
            //if (response != null)
            //{
                return new JsonResult(new { message = response });
            //}
            //else
            //{
                //return new JsonResult(new { message = response });
           //}
        }
    }
}
