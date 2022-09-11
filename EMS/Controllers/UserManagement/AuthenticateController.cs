using EMS.Core.Entities.UserManagement;
using EMS.Core.Services.GenericService;
using EMS.Core.ViewModelEntitity;
using EMS.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMS.Controllers.UserManagement
{
    public class AuthenticateController : Controller
    {
        private readonly IGenericService<Users, int> _IUserService;

        public AuthenticateController(IGenericService<Users, int> userService)
        {
            _IUserService = userService;
        }
        public IActionResult Login(AuthenticateModel model)
        {
            model = model == null ? new AuthenticateModel() : model;
            ModelState.Remove("Password");
            ModelState.Remove("UserName");
            return View(ViewHelpers.GetViewName("UserManagement", "Login"), model);
        }

        [HttpPost]
        public async Task<IActionResult> LoginPost(AuthenticateModel model)
        {
            var responseModel = await _IUserService.GetSingle(x => x.UserName.Trim().ToLower() == model.UserName.Trim().ToLower()
            && x.Password.Trim().ToLower() == model.Password.Trim().ToLower());

            if (responseModel != null)
            {
                HttpContext.Session.SetString("UserName", responseModel.UserName);
                HttpContext.Session.SetString("RoleName", responseModel.RoleName);
                return RedirectToAction("Index", "Home");

            }
            model.Password = string.Empty;
            model.Message = "Invalid User Name or Password !";
            return RedirectToAction("Login", "Authenticate", model);
        }

        public async Task<IActionResult> LogOut()
        {
            HttpContext.Session.Clear();
            return await Task.Run(() => RedirectToAction("Login", "Authenticate", new AuthenticateModel()));

        }
    }
}
