using EMS.Core.Entities.Survey;
using EMS.Core.Services.GenericService;
using EMS.Core.Services.Survey;
using EMS.Core.ViewModelEntitity.Survey;
using EMS.Helpers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMS.Controllers.Survey
{
    public class MonitoringController : Controller
    {
        private readonly IGenericService<MonitoringDetails, int> _IMontiringDetailService;
        private readonly ISurveyService __ISurveyService;
        private readonly IGenericService<Monitoring, int> _IMontiringService;


        public MonitoringController(IGenericService<MonitoringDetails, int> monitoringService, 
            ISurveyService serviceSurvey, IGenericService<Monitoring, int> monitoringGenericService)
        {
            _IMontiringDetailService = monitoringService;
            __ISurveyService = serviceSurvey;
            _IMontiringService = monitoringGenericService;
        }
        public async Task<IActionResult> Index()
        {
            var response = await __ISurveyService.GetMonitoringAndDetailsList();
            return View(ViewHelpers.GetViewName("Survey", "Monitoring"), response);
        }

        public async Task<IActionResult> UpdateTaskStatus(int taskId, string taskName, string status)
        {
            var model = new UpdateTaskVm()
            {
                TaskId = taskId,
                PreviousStatus = status,
                TaskName = taskName
            };

            return await Task.Run(() => PartialView(ViewHelpers.GetViewName("Survey", "TaskManagerUpdatePartial"), model));
        }
        public async Task<IActionResult> GetMonitorDetails(int id)
        {
            var response = await _IMontiringDetailService.GetList(x => x.IsActive && !x.IsDeleted && x.MonitoringId == id);
            return PartialView(ViewHelpers.GetViewName("Survey", "MonitorDetailPartial"), response);
        }

        public async Task<IActionResult> SaveMonitoryStatus(string id)
        {
            var ids = id.Split(",");
            var responseModels = new List<Monitoring>();
            ids.ToList().ForEach(data => {
                var model = _IMontiringService.GetSingle(x => x.Id == Convert.ToInt32(data)).Result;
                model.IsComplete = true;
                responseModels.Add(model);
            });

            var UpadateResponse = await _IMontiringService.UpdateEntities(responseModels.ToArray());
            return Json("Data has been Updated !");
        }
    }
}
