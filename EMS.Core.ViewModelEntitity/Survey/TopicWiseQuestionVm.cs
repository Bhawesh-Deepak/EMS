using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Core.ViewModelEntitity.Survey
{
    public class TopicWiseQuestionVm
    {
        public int QuestionId { get; set; }
        public string Question { get; set; }
        public int OptionId { get; set; }
        public string OptionValue { get; set; }
        public int ChildQuestionId { get; set; }
        public int ChildOptionId { get; set; }
        public int ChildQuestionId1 { get; set; }
        public int ChildOptionId1 { get; set; }
        public string TopicName { get; set; }
    }
}
