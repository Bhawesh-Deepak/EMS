using EMS.Core.Entities.Survey;
using EMS.Core.Services.GenericService;
using EMS.Core.Services.Survey;
using EMS.Core.ViewModelEntitity.Survey;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Infrastructure.Services.SurveyImplementation
{
    public class SurveyService : ISurveyService
    {
        private readonly IGenericService<Questions, int> _IQuestionService;
        private readonly IGenericService<Options, int> _IOptionsService;

        public SurveyService(IGenericService<Questions, int> questionService,
            IGenericService<Options, int> optionsService)
        {
            _IQuestionService = questionService;
            _IOptionsService = optionsService;

        }

        public async Task<bool> CreateQuestionOptionsForSurvey(SurveyVm model, List<string> optionsValue)
        {
            var questionResponse = await _IQuestionService.CreateEntity(model.QuestionDetail);
            if (questionResponse)
            {
                var questionId = (await _IQuestionService.GetList(x => !x.IsDeleted)).Max(x => x.Id);
                var optionModels = new List<Options>();
                optionsValue.ToList().ForEach(data =>
                {
                    optionModels.Add(new Options()
                    {
                        QuestionId = questionId,
                        OptionValue = data,
                        IsActive = true,
                        IsDeleted = false
                    });
                });

                var optionResponse = await _IOptionsService.CreateEntities(optionModels.ToArray());

                return optionResponse;

            }
            return questionResponse;
        }

        public async Task<SurveyVm> GetSurveyDetails(int questionId)
        {
            var questionModel = await _IQuestionService.GetSingle(x => x.Id == questionId);
            var optionModels = await _IOptionsService.GetList(x => x.QuestionId == questionId && !x.IsDeleted);

            var surveyModel = new SurveyVm()
            {
                Options = optionModels.ToList(),
                QuestionDetail = questionModel
            };

            return surveyModel;
        }

        public async Task<List<Questions>> GetQuestionDetails(string topicName)
        {
            if (!string.IsNullOrEmpty(topicName))
            {
                var topicWiseQuestionList = await _IQuestionService.GetList(x => x.TopicName.Trim().ToLower() == topicName.Trim().ToLower());
                return topicWiseQuestionList.ToList();
            }
            var response = await _IQuestionService.GetList(x => !x.IsDeleted);
            return response.ToList();
        }

        public async Task<List<Options>> GetOptionsList(int questionId)
        {
            var response = await _IOptionsService.GetList(x => x.IsActive && !x.IsDeleted && x.QuestionId == questionId);
            return response.ToList();
        }

        public async Task<bool> DeleteQuestion(int id)
        {
            var deleteQuestionModel = await _IQuestionService.GetSingle(x => x.Id == id);
            deleteQuestionModel.IsActive = false;
            deleteQuestionModel.IsDeleted = true;

            var deleteOptions = await _IOptionsService.GetList(x => x.QuestionId == id);
            deleteOptions.ToList().ForEach(data =>
            {
                data.IsActive = false;
                data.IsDeleted = true;
            });

            var deleteQuestionResponse = await _IQuestionService.UpdateEntity(deleteQuestionModel);
            if (deleteQuestionResponse)
            {
                var deleteOptionResponse = await _IOptionsService.UpdateEntities(deleteOptions.ToArray());
                return deleteOptionResponse;
            }

            return deleteQuestionResponse;
        }

        public async Task<bool> UpdateSurveyQuestion(SurveyVm model, List<string> optionsValue)
        {
            model.QuestionDetail.IsActive = true;
            model.QuestionDetail.IsDeleted = false;
            model.QuestionDetail.UpdatedBy = 1;
            model.QuestionDetail.UpdatedDate = DateTime.Now;

            var updateQuestion = await _IQuestionService.UpdateEntity(model.QuestionDetail);

            if (updateQuestion) 
            {
                var deletePreviousOptions = await _IOptionsService.GetList(x => x.QuestionId == model.QuestionDetail.Id);
                deletePreviousOptions.ToList().ForEach(data =>
                {
                    data.IsActive = false;
                    data.IsDeleted = true;
                    data.UpdatedDate = DateTime.Now;
                });
                var deleteOptionResponse = await _IOptionsService.UpdateEntities(deletePreviousOptions.ToArray());

                if (deleteOptionResponse) {

                    var optionModels = new List<Options>();

                    optionsValue.ToList().ForEach(data =>
                    {
                        optionModels.Add(new Options()
                        {
                            QuestionId = model.QuestionDetail.Id,
                            OptionValue = data,
                            IsActive = true,
                            IsDeleted = false,

                        });
                    });

                    var optionResponse = await _IOptionsService.CreateEntities(optionModels.ToArray());

                    return optionResponse;
                }

            }

            return updateQuestion;
        }
    }
}
