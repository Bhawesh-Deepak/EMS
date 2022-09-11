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
    public class SurveyController : Controller
    {
        private readonly ISurveyService __ISurveyService;

        public SurveyController(ISurveyService surveyService)
        {
            __ISurveyService = surveyService;
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

                return Json(ResponseHelper.ResponseMessage(response,OperationType.Create));
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
            var response = await __ISurveyService.GetOptionsList(questionId);
            return PartialView(ViewHelpers.GetViewName("Survey", "OptionDetailPartial"), response);
        }

        public async Task<IActionResult> DeleteRecord(int id)
        {
            var deleteResponse = await __ISurveyService.DeleteQuestion(id);
            return deleteResponse ? Json(ResponseHelper.ResponseMessage(true,OperationType.Delete)) : Json(false, OperationType.Delete);


        }
    }
}
