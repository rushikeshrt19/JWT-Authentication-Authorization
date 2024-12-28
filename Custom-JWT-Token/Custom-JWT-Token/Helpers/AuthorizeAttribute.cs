using Custom_JWT_Token.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Custom_JWT_Token.Helpers
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class Authorization : Attribute, IAuthorizationFilter
    {
        private readonly IList<Role> _role;
        public Authorization(params Role[] _roles)
        {
            this._role = _roles ?? new Role[] { };
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var isRolePermission = false;
            User user = (User)context.HttpContext.Items["User"];
            if (user == null)
            {
                context.Result = new JsonResult
                    (new { message = "UnAuthorized" }) 
                { StatusCode = StatusCodes.Status401Unauthorized };
            }
            if (user != null && this._role.Any())
            {
                foreach (var role in user.userRole)
                {
                    foreach (var authRole in this._role)
                    {
                        if(authRole == role) 
                            isRolePermission = true;
                    }
                }
            }
            if (!isRolePermission)
            {
                context.Result= new JsonResult (new {message="UnAuthorized"}) { StatusCode = StatusCodes.Status401Unauthorized};
            }
        }
    }
}
