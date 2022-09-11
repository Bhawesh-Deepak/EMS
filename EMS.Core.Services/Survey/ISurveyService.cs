using EMS.Core.Entities.Survey;
using EMS.Core.ViewModelEntitity.Survey;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Core.Services.Survey
{
    public interface ISurveyService
    {
        Task<SurveyVm> GetSurveyDetails(int questionId);
        Task<bool> CreateQuestionOptionsForSurvey(SurveyVm model, List<string> optionsValue);
        Task<List<Questions>> GetQuestionDetails(string topicName);
        Task<List<Options>> GetOptionsList(int questionId);
        Task<bool> DeleteQuestion(int id);
        Task<bool> UpdateSurveyQuestion(SurveyVm model, List<string> optionsValue);

    }
}
