using EMS.Core.Entities.Survey;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Core.ViewModelEntitity.Survey
{
    public class SurveyVm
    {
        public Questions QuestionDetail { get; set; }
        public List<Options> Options { get; set; }
    }
}
