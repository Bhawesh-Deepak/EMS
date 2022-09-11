using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EMS.Core.Entities.Master;
using EMS.Core.Services.GenericService;
using EMS.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace EMS.Controllers.Master
{
    public class SeasionController : Controller
    {
        private readonly IGenericService<SeasonModel, int> _ISessionService;

        public SeasionController(IGenericService<SeasonModel, int> seasionService)
        {
            _ISessionService = seasionService;

        }
        public async Task<IActionResult> SeasinList()
        {
            var response = await _ISessionService.GetList(x => !x.IsDeleted);
            return View(ViewHelpers.GetViewName("Master/SeasionMaster", "SeasionList"), response);
        }

        public async Task<IActionResult> CreateSeason(int id)
        {
            var responseModel = await _ISessionService.GetSingle(x => x.Id == id);
            return PartialView(ViewHelpers.GetViewName("Master/SeasionMaster", "SeasonCreate"), responseModel);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSeasonPost(SeasonModel model)
        {
            model.IsActive = true;
            model.IsDeleted = false;
            model.UpdatedDate = DateTime.Now;

            if (model.Id == 0)
            {
                var response = await _ISessionService.CreateEntity(model);
                return Json(ResponseHelper.ResponseMessage(response, Core.Entities.Common.OperationType.Create));
            }
            else
            {
                var response = await _ISessionService.UpdateEntity(model);
                return Json(ResponseHelper.ResponseMessage(response, Core.Entities.Common.OperationType.Update));
            }
        }

        public async Task<IActionResult> DeleteSeason(int id)
        {
            var deleteModel = await _ISessionService.GetSingle(x => x.Id == id);
            deleteModel.IsActive = false;
            deleteModel.IsDeleted = true;

            var response = await _ISessionService.UpdateEntity(deleteModel);
            return Json(ResponseHelper.ResponseMessage(response, Core.Entities.Common.OperationType.Delete));
        }
    }
}
