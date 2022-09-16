using EMS.Core.Entities.Survey;
using EMS.Core.Services.GenericService;
using EMS.Core.ViewModelEntitity.Survey;
using EMS.Helpers;
using EMS.Infrastructure.Repository.Common;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMS.Controllers.Survey
{
    public class TopicWiseQuestionController : Controller
    {
        private readonly IDapperService<TopicQuestionParams> _ITopicQuestionService;
        private readonly IGenericService<Questions, int> _IQuestionService;
        private readonly IGenericService<Options, int> _IOptionService;

        public TopicWiseQuestionController(IDapperService<TopicQuestionParams> topicQuestionService, IGenericService<Questions, int> questionService, IGenericService<Options, int> optionsService)
        {
            _ITopicQuestionService = topicQuestionService;
            _IQuestionService = questionService;
            _IOptionService = optionsService;
        }
        public IActionResult QuestionDetails(string topicName)
        {
            topicName = "TestTopic";
            var response = _ITopicQuestionService.GetAll<TopicWiseQuestionVm>(SqlConstant.GetQuestionDetails, new TopicQuestionParams()
            {
                TopicName = topicName
            });
            response.ToList().ForEach(data =>
            {
                data.TopicName = topicName;
            });

            return View(ViewHelpers.GetViewName("QuestionDetails", "QuestionDetailsPartial"), response);
        }

        public async Task<IActionResult> GetChildQuestion(int questionId, int optionId)
        {
            var optionList = new List<Options>();
            var response = new List<TopicWiseQuestionVm>();

            var childQuestion = await _IQuestionService.GetSingle(x => x.Id == questionId && x.OptionId == optionId);
            if (childQuestion == null)
            {
                childQuestion = await _IQuestionService.GetSingle(x => x.Id == questionId && x.ChildOptionId1 == optionId);
            }
            if (childQuestion != null)
            {
                optionList = (await _IOptionService.GetList(x => x.QuestionId == questionId && x.IsActive && !x.IsDeleted)).ToList();

                var responseModel = new List<TopicWiseQuestionVm>();

                optionList.ToList().ForEach(data =>
                {
                    responseModel.Add(new TopicWiseQuestionVm()
                    {
                        QuestionId = questionId,
                        Question = childQuestion.Question,
                        OptionId = data.Id,
                        OptionValue = data.OptionValue
                    });
                });
                return PartialView(ViewHelpers.GetViewName("QuestionDetails", "ChildQuestionPartial"), responseModel);
            }
            return PartialView(ViewHelpers.GetViewName("QuestionDetails", "ChildQuestionPartial"));
        }
    }
}
