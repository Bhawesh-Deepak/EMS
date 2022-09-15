using EMS.Core.Entities.Survey;
using EMS.Core.Services.GenericService;
using EMS.Core.ViewModelEntitity.Survey;
using EMS.Helpers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMS.Controllers.Survey
{
    public class TaskMasterController : Controller
    {
        private readonly IGenericService<TaskMaster, int> _ITaskMasterService;

        public TaskMasterController(IGenericService<TaskMaster, int> taskMasterService)
        {
            _ITaskMasterService = taskMasterService;
        }
        public async Task<IActionResult> Index()
        {
            var response = await _ITaskMasterService.GetList(x => !x.IsDeleted);
            return View(ViewHelpers.GetViewName("Survey", "TaskManager"), response);
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
        [HttpPost]
        public async Task<IActionResult> PostUpdateTaskStatus(UpdateTaskVm model)
        {
            var taskModel = await _ITaskMasterService.GetSingle(x => x.Id == model.TaskId);
            taskModel.TaskStatus = model.StatusId;
            taskModel.Comment = model.Comment;

            var response = await _ITaskMasterService.UpdateEntity(taskModel);
            return Json(ResponseHelper.ResponseMessage(response, Core.Entities.Common.OperationType.Update));
        }
    }
}
