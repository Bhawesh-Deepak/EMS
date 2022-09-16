using EMS.Core.Entities.Common;
using EMS.Core.Entities.UserManagement;
using EMS.Core.Entities.Utilities;
using EMS.Core.Services.GenericService;
using EMS.Helpers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMS.Controllers.UserManagement
{
    [AuthenticateService]
    public class UsersController : Controller
    {
        private readonly IGenericService<Users, int> _IUserService;
        private readonly IGenericService<RoleMaster, int> _IRoleMasterService;

        public UsersController(IGenericService<Users, int> userService, IGenericService<RoleMaster, int>  roleService)
        {
            _IUserService = userService;
            _IRoleMasterService = roleService;
        }
        public async Task<IActionResult> Index()
        {
            var response = await _IUserService.GetList(x => !x.IsDeleted);
            return View(ViewHelpers.GetViewName("UserManagement", "UserList"), response);
        }

        public async Task<IActionResult> CreateUser(int id)
        {
            ViewBag.RoleList = await _IRoleMasterService.GetList(x => x.IsActive && !x.IsDeleted);
            var userDetail = await _IUserService.GetSingle(x => x.Id == id);
            return PartialView(ViewHelpers.GetViewName("UserManagement", "_CreateUsersPartial"), userDetail);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUserPost(Users model)
        {
            if (model.Id == 0)
            {
                var response = await _IUserService.CreateEntity(model);
                return Json(ResponseHelper.ResponseMessage(response,OperationType.Create));
            }
            else
            {
                var response = await _IUserService.UpdateEntity(model);
                return Json(ResponseHelper.ResponseMessage(response, OperationType.Update));
            }
        }

        public async Task<IActionResult> DeleteRecord(int id)
        {
            var response = await _IUserService.GetSingle(x => x.Id == id);
            if (response != null)
            {
                response.IsDeleted = true;
                var deleteResponse = await _IUserService.UpdateEntity(response);
                return Json(ResponseHelper.ResponseMessage(deleteResponse, OperationType.Delete));
            }

            return Json("Record do not found !");
        }
    }
}
