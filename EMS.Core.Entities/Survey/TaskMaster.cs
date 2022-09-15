using EMS.Core.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Core.Entities.Survey
{
    [Table("TaskMaster", Schema = "Survey")]
    public class TaskMaster:Base<int>
    {
        public string SeasonName { get; set; }
        public string ZoneName { get; set; }
        public string EventName { get; set; }
        public string TaskName { get; set; }

        public string AgencyName { get; set; }
        public string ContactReason { get; set; }

        public string TaskStatus { get; set; }
        public string Comment { get; set; }
    }
}
