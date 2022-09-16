using EMS.Core.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Core.Entities.UserManagement
{
    [Table("Modules", Schema = "UserManagement")]
    public class ModuleMaster: Base<int>
    {
        public string ModuleName { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public string Icon { get; set; }
        public int DisplayOrder { get; set; }
        [NotMapped]
        public bool  IsMapped { get; set; }
        [NotMapped]
        public string  Role { get; set; }
    }
}
