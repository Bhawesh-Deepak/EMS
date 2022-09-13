using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EMS.Core.Entities.Master;
using EMS.Core.Entities.Survey;
using EMS.Core.Services.GenericService;
using EMS.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace EMS.Controllers.Survey
{
    public class EventController : Controller
    {
        private readonly IGenericService<SeasonModel, int> _ISeasonService;
        private readonly IGenericService<RegionMaster, int> _IRegionService;
        private readonly IGenericService<EventMaster, int> _IEventMasterService;

        public EventController(IGenericService<SeasonModel, int> seasonService,
            IGenericService<RegionMaster, int> regionService, IGenericService<EventMaster, int> eventMasterService)
        {
            _ISeasonService = seasonService;
            _IRegionService = regionService;
            _IEventMasterService = eventMasterService;
        }
        public async Task<IActionResult> CreateEvent(int id)
        {
            await PopulateViewBag();
            var responseModel = await _IEventMasterService.GetList(x => !x.IsDeleted);
            return View(ViewHelpers.GetViewName("Event", "EventCreate"));
        }

        #region PopulateViewBag

        private async Task PopulateViewBag()
        {
            ViewBag.SeasonList = await _ISeasonService.GetList(x => !x.IsDeleted);
            ViewBag.RegionList = await _IRegionService.GetList(x => !x.IsDeleted);
        }

        #endregion
    }
}
