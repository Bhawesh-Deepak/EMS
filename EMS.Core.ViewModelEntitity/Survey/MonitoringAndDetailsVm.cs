using EMS.Core.Entities.Survey;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Core.ViewModelEntitity.Survey
{
    public class MonitoringAndDetailsVm
    {
        public int MonitoringId { get; set; }
        public string SeasonName { get; set; }
        public string ZoneName { get; set; }
        public string EventName { get; set; }
        public string EventPercentage { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Comment { get; set; }
        
        public List<MonitoringDetails> lstMonitoringDetails { get; set; }
    }
}
