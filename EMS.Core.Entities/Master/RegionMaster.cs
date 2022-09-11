using EMS.Core.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Core.Entities.Master
{
    [Table("Region", Schema = "Master")]
    public class RegionMaster: Base<int>
    {
        public int SeasonId { get; set; }
        public string AreaName { get; set; }
        public DateTime RegionStartDate { get; set; }
        public DateTime ZoneEndDate { get; set; }
        public DateTime ZoneStartDate { get; set; }
        public DateTime EndTimeZone { get; set; }
        public string AreaDescription { get; set; }
        public int AreaCapacity { get; set; }
        public string AreaManagerEmail { get; set; }
        public int ExpectedAttende { get; set; }
        public int Space { get; set; }
        public string TargetGroup { get; set; }
        public string AreaManagerName { get; set; }
        public string DistrictManagerMobile { get; set; }
        public string Sex { get; set; }
    }
}
