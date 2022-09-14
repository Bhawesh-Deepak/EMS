using EMS.Core.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Core.Entities.Utilities
{
    [Table("Role", Schema = "Master")]
    public class RoleMaster: Base<int>
    {
        public string RoleName { get; set; }
        public string RoleDescription { get; set; }
    }
}
