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

        public TopicWiseQuestionController(IDapperService<TopicQuestionParams> topicQuestionService)
        {
            _ITopicQuestionService = topicQuestionService;
        }
        public IActionResult QuestionDetails(string topicName)
        {
            topicName = "TestTopic";
            var response = _ITopicQuestionService.GetAll<TopicWiseQuestionVm>(SqlConstant.GetQuestionDetails, new TopicQuestionParams()
            {
                TopicName = topicName
            });
            return View(ViewHelpers.GetViewName("QuestionDetails", "QuestionDetailsPartial"),response);
        }
    }
}
