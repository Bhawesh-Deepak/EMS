using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EMS.Core.Entities.Master;
using EMS.Core.Services.GenericService;
using EMS.Core.Services.Master;
using EMS.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace EMS.Controllers.Master
{
    public class RegionMasterController : Controller
    {
        private readonly IGenericService<RegionMaster, int> _IRegionMasterService;
        private readonly IGenericService<SeasonModel, int> _ISeasonService;
        private readonly IMasterService _IMasterService;
        public RegionMasterController(IGenericService<RegionMaster, int> regionMasterService,
            IGenericService<SeasonModel, int> seasonService, IMasterService masterService)
        {
            _IRegionMasterService = regionMasterService;
            _ISeasonService = seasonService;
            _IMasterService = masterService;
        }
        public async Task<IActionResult> RegionList()
        {
            var responseModel = await _IMasterService.GetRegionMasterList();
            return View(ViewHelpers.GetViewName("Master/RegionMaster", "RegionList"), responseModel);
        }

        public async Task<IActionResult> CreateRegionMaster(int id)
        {
            await PopulateViewBag();

            var regionMaster = await _IRegionMasterService.GetSingle(x => x.Id == id);
            return PartialView(ViewHelpers.GetViewName("Master/RegionMaster", "CreateRegionMaster")
                , regionMaster);
        }

        [HttpPost]
        public async Task<IActionResult> CreateRegionMasterPost(RegionMaster model)
        {
            model.IsActive = true;
            model.IsDeleted = false;
            model.UpdatedDate = DateTime.Now;

            if (model.Id == 0)
            {
                var response = await _IRegionMasterService.CreateEntity(model);
                return Json(ResponseHelper.ResponseMessage(response, Core.Entities.Common.OperationType.Create));
            }
            else
            {
                var response = await _IRegionMasterService.UpdateEntity(model);
                return Json(ResponseHelper.ResponseMessage(response, Core.Entities.Common.OperationType.Update));
            }
        }

        #region PrivateFields
        private async Task PopulateViewBag()
        {
            ViewBag.SeasionList = await _ISeasonService.GetList(x => !x.IsDeleted);
        }

        public async Task<IActionResult> RegionListBySessionId(int SessionId)
        {
            var responseModel = await _IMasterService.GetRegionMasterList();
            return Json(responseModel.Where(x=>x.SessionId==SessionId));
        }

        #endregion
    }
}
