using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMS.Helpers
{
    [AllowAnonymous]
    public class AuthenticateService: TypeFilterAttribute
    {
        public AuthenticateService() : base(typeof(AuthenticateUser))
        {

        }
    }

    public class AuthenticateUser : IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var url = context.HttpContext.Request.GetDisplayUrl();
            if (context.HttpContext.Session.GetString("UserName") == null)
            {
                context.Result = new RedirectToActionResult("Login", "Authenticate", new { returnUrl = url });
            }
        }
    }
}
