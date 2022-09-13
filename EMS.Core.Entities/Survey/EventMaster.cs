using EMS.Core.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Core.Entities.Survey
{
    [Table("Event",Schema ="Survey")]
    public class EventMaster: Base<int>
    {
        public string EventName { get; set; }
        public string CompanyName { get; set; }
        public int AreaId { get; set; }
        public int SeasonId { get; set; }
        public DateTime EventStartDate { get; set; }
        public DateTime EventEndDate { get; set; }
        public string TargetAudience { get; set; }
        public int DailyCapacity { get; set; }
    }
}
