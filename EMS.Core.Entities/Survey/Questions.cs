using EMS.Core.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Core.Entities.Survey
{
    [Table("Questions", Schema = "Survey")]
    public class Questions: Base<int>
    {
        [Required(ErrorMessage ="Topic name is required !")]
        public string TopicName { get; set; }

        [Required(ErrorMessage = "Question is required !")]
        public string Question { get; set; }

        public int ChildQuestionId { get; set; }
        public int OptionId { get; set; }
        public int ChildQuestionId1 { get; set; }
        public int ChildOptionId1 { get; set; }
    }
}
