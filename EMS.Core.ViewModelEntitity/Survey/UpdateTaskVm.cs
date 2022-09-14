using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Core.ViewModelEntitity.Survey
{
    public class UpdateTaskVm
    {
        public int TaskId { get; set; }
        public string StatusId { get; set; }
        public string Comment { get; set; }
        public string TaskName { get; set; }
        public string PreviousStatus { get; set; }
    }
}
