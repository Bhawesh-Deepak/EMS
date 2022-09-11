using EMS.Core.ViewModelEntitity.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Core.Services.Master
{
    public interface IMasterService
    {
        Task<List<RegionMasterVm>> GetRegionMasterList();
    }
}
