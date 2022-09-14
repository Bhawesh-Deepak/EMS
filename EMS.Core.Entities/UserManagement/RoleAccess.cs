using EMS.Core.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Core.Entities.UserManagement
{
    [Table("RoleAccess", Schema = "UserManagement")]
    public class RoleAccess: Base<int>
    {
        public int RoleId { get; set; }
        public int ModuleId { get; set; }
    }
}
