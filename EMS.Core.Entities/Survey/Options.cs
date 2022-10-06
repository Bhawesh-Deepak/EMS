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
    [Table("Options", Schema= "Survey")]
    public class Options: Base<int>
    {
        public int QuestionId { get; set; }

        [Required(ErrorMessage ="OptionValue is required !")]
        public string OptionValue { get; set; }
        public string TaskId { get; set; }
    }
}
