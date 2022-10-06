using EMS.Core.Entities.Common;
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
    [AuthenticateService]
    public class SurveyController : Controller
    {
        private readonly ISurveyService __ISurveyService;
        private readonly IGenericService<Questions, int> _IQuestionService;
        private readonly IGenericService<Options, int> _IOptionsService;
        private readonly IGenericService<TaskMaster, int> _ITaskMasterService;
        public SurveyController(ISurveyService surveyService,
            IGenericService<Questions, int> questionService, IGenericService<Options, int> optionService, IGenericService<TaskMaster, int> iTaskMasterService)
        {
            __ISurveyService = surveyService;
            _IQuestionService = questionService;
            _IOptionsService = optionService;
            _ITaskMasterService = iTaskMasterService;
        }

        public async Task<IActionResult> Index(int id)
        {
            var model = await __ISurveyService.GetSurveyDetails(id);
            return View(ViewHelpers.GetViewName("Survey", "QuestionOptionsView"), model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSurveyQuestion(SurveyVm model)
        {
            string optionsValue = Request.Form["Option"];
            List<string> options = new List<string>();
            if (!string.IsNullOrEmpty(optionsValue))
            {
                options = optionsValue.Split(",").ToList<string>();
            }
            if (model.QuestionDetail.Id == 0)
            {
                var response = await __ISurveyService.CreateQuestionOptionsForSurvey(model, options);

                return Json(ResponseHelper.ResponseMessage(response, OperationType.Create));
            }
            else
            {
                var updateResponse = await __ISurveyService.UpdateSurveyQuestion(model, options);
                return Json(ResponseHelper.ResponseMessage(updateResponse, OperationType.Update));
            }

        }

        public async Task<IActionResult> GetQuestionDetails(string topicName)
        {
            var response = await __ISurveyService.GetQuestionDetails(topicName);
            return View(ViewHelpers.GetViewName("Survey", "QuestionList"), response);
        }

        public async Task<IActionResult> GetOptionsList(int questionId)
        {
            ViewBag.TaskList = await _ITaskMasterService.GetAll(x => x.IsActive && !x.IsDeleted);
            var response = await __ISurveyService.GetOptionsList(questionId);
            return PartialView(ViewHelpers.GetViewName("Survey", "OptionDetailPartial"), response);
        }

        public async Task<IActionResult> TaskOptionMapping(string optionIds, string option1Values, string option2Values)
        {
            if (optionIds == null || option1Values == null || option2Values == null)
            {
                return Json("Please select Option and Task for mapping");
            }
            var optionList = optionIds.Split(",").ToList();

            for (int i = 0; i < optionList.Count; i++)
            {
                if (i == 0)
                {
                    var optionModel = await _IOptionsService.GetSingle(x => x.Id == Convert.ToInt32(optionList[i]));
                    optionModel.TaskId = option1Values;
                    var updateResponse = await _IOptionsService.UpdateEntity(optionModel);
                }
                else if(i==1)
                {
                    var optionModel = await _IOptionsService.GetSingle(x => x.Id == Convert.ToInt32(optionList[i]));
                    optionModel.TaskId = option2Values;
                    var updateResponse = await _IOptionsService.UpdateEntity(optionModel);
                }

            }

            return Json("Task option mapping successfully ! Completed !");
        }
        public async Task<IActionResult> DeleteRecord(int id)
        {
            var deleteResponse = await __ISurveyService.DeleteQuestion(id);
            return deleteResponse ? Json(ResponseHelper.ResponseMessage(true, OperationType.Delete)) : Json(false, OperationType.Delete);


        }

        public async Task<IActionResult> CreateParentChildQuestion()
        {
            var questionDetails = await _IQuestionService.GetList(x => !x.IsDeleted);
            ViewBag.QuestionList = questionDetails;
            var optionsList = await _IOptionsService.GetList(x => !x.IsDeleted);
            ViewBag.options = optionsList;
            return View(ViewHelpers.GetViewName("Survey", "ParentChildQuestion"));

        }


        public async Task<IActionResult> CreateParentChildQuestionPost(int parentId, int optionId, int childQuestionid, int childQuestion1Id, int parentOptionId1)
        {
            if (parentId > 0 || optionId > 0 || childQuestionid > 0)
            {
                var parentQuestion = await _IQuestionService.GetSingle(x => x.Id == parentId);
                parentQuestion.ChildQuestionId = childQuestionid;
                parentQuestion.OptionId = optionId;
                parentQuestion.ChildQuestionId1 = childQuestion1Id;
                parentQuestion.ChildOptionId1 = childQuestion1Id == 0 ? 0 : parentOptionId1;

                var updateResponse = await _IQuestionService.UpdateEntity(parentQuestion);
                return Json(ResponseHelper.ResponseMessage(updateResponse, OperationType.Create));
            }
            return Json("Please select Parent Question Child Question and Parent Option !");

        }

        public async Task<IActionResult> GetOptionList(int questionId)
        {
            var optionList = await _IOptionsService.GetList(x => x.QuestionId == questionId && !x.IsDeleted);
            return Json(optionList);
        }

        //public async Task<IActionResult> GetParentChildQuestion()
        //{


        //}
    }
}
