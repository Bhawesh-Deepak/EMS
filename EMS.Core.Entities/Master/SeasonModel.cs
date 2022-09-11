using EMS.Core.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Core.Entities.Master
{
    [Table("Season", Schema = "Master")]
    public class SeasonModel: Base<int>
    {
        public string SeasonName { get; set; }
        public DateTime SeasonStartDate { get; set; }
        public DateTime SeasonEndDateDate { get; set; }
        public string ManagerName { get; set; }
        public string ManagerMobileNumber { get; set; }
        public string ManagerEmail { get; set; }
        public string Sex { get; set; }
    }
}
