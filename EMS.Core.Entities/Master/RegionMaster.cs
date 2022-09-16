using EMS.Core.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        [Required(ErrorMessage ="Area name is required !")]
        [Display(Prompt ="Please enter Area Name")]
        public string AreaName { get; set; }

        [Required(ErrorMessage ="Region Start Date is required !")]
        public DateTime RegionStartDate { get; set; }
        public DateTime ZoneEndDate { get; set; }
        public DateTime ZoneStartDate { get; set; }
        public DateTime EndTimeZone { get; set; }

        [Display(Prompt ="Please enter Area Description")]
        public string AreaDescription { get; set; }

        [Display(Prompt ="Please enter Area Capacity")]
        public int AreaCapacity { get; set; }

        [Display(Prompt ="Please enter Area Manager Email ")]
        public string AreaManagerEmail { get; set; }

        [Display(Prompt ="Please enter Expected Attende")]
        public int ExpectedAttende { get; set; }

        [Display(Prompt ="Please enter Space capacity")]
        public int Space { get; set; }
        public string TargetGroup { get; set; }
        public string AreaManagerName { get; set; }
        public string DistrictManagerMobile { get; set; }
        public string Sex { get; set; }
    }
}
