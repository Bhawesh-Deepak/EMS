using EMS.Core.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Core.Entities.Survey
{
    [Table("Monitoring", Schema = "Survey")]
    public class Monitoring:Base<int>
    {
        public string SeasonName { get; set; }
        public string ZoneName { get; set; }
        public string EventName { get; set; }
        public string Percentage { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Comment { get; set; }
        public bool IsComplete { get; set; }

    }
}
