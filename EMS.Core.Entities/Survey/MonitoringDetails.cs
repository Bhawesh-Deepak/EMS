using EMS.Core.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Core.Entities.Survey
{
    [Table("MonitoringDetails", Schema = "Survey")]
    public class MonitoringDetails:Base<int>
    {
        public int MonitoringId { get; set; }
        public string AgancyName { get; set; }
        public string Percentage { get; set; }
        
        

    }
}
