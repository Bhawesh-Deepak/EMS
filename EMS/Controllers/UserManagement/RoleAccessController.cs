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
    public class RoleAccessController : Controller
    {
        private readonly IGenericService<RoleMaster, int> _IRoleMasterService;
        private readonly IGenericService<ModuleMaster, int> _IModuleMasterService;
        private readonly IGenericService<RoleAccess, int> _IRoleAccessService;

        public RoleAccessController(IGenericService<RoleMaster, int> roleMasterService,
            IGenericService<ModuleMaster, int> moduleMasterService, IGenericService<RoleAccess, int> roleAccessService)
        {
            _IRoleAccessService = roleAccessService;
            _IModuleMasterService = moduleMasterService;
            _IRoleMasterService = roleMasterService;
        }
        public async Task<IActionResult> RoleAccess()
        {
            ViewBag.RoleList = await _IRoleMasterService.GetList(x => !x.IsDeleted);
            return View(ViewHelpers.GetViewName("UserManagement", "RoleAccessIndex"));
        }

        public async Task<IActionResult> RoleAccessDetails(int roleId)
        {
            var moduleMaster = await _IModuleMasterService.GetList(x => !x.IsDeleted);
            var roleAccess = await _IRoleAccessService.GetList(x => !x.IsDeleted && x.RoleId == roleId);
            moduleMaster.ToList().ForEach(data =>
            {
                data.RoleId = roleId;
                if (roleAccess.Any())
                {
                    roleAccess.ToList().ForEach(item =>
                    {
                        if (item.ModuleId == data.Id)
                        {
                            data.IsMapped = true;
                        }
            
                    });
                }

            });

            return PartialView(ViewHelpers.GetViewName("UserManagement", "RoleAccessList"), moduleMaster);
        }

        [HttpPost]
        public async Task<IActionResult> CreateRoleAccess(string[] moduleId, int roleId)
        {
            var roleAccessList = await _IRoleAccessService.GetList(x => !x.IsDeleted && x.RoleId == roleId);
            roleAccessList.ToList().ForEach(data =>
            {
                data.IsDeleted = true;
                data.IsActive = false;
            });
            var roleDeleteResponse = await _IRoleAccessService.UpdateEntities(roleAccessList.ToArray());
            var roleAccess = new List<RoleAccess>();
            moduleId.ToList().ForEach(data =>
            {
                roleAccess.Add(new RoleAccess()
                {
                    ModuleId = Convert.ToInt32(data),
                    RoleId = roleId,
                    IsActive = true,
                    IsDeleted = false,
                    CreatedBy = 1,
                    CreatedDate = DateTime.Now

                });
            });

            var addRoleAccess = await _IRoleAccessService.CreateEntities(roleAccess.ToArray());

            return Json("Role Access created !");
        }
    }
}
