using EMS.Core.Entities.Common;
using EMS.Core.Entities.Utilities;
using EMS.Core.Services.GenericService;
using EMS.Helpers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMS.Controllers.Utilities
{
    public class RoleMasterController : Controller
    {
        private readonly IGenericService<RoleMaster, int> _IRoleService;

        public RoleMasterController(IGenericService<RoleMaster, int> roleService)
        {
            _IRoleService = roleService;
        }
        public async Task<IActionResult> RoleList()
        {
            var response = await _IRoleService.GetList(x =>x.IsDeleted==false);
            return View(ViewHelpers.GetViewName("Utilities", "RoleList"), response);
        }

        public async Task<IActionResult> CreateRole()
        {
            return await Task.Run(() => PartialView(ViewHelpers.GetViewName("Utilities", "CreateRole")));
        }

        [HttpPost]
        public async Task<IActionResult> CreateRolePost(RoleMaster model)
        {
            model.IsActive = true;
            model.IsDeleted = false;
            var response = await _IRoleService.CreateEntity(model);
            return Json(ResponseHelper.ResponseMessage(response, OperationType.Create));
        }
    }
}
