using EMS.Core.Entities.Master;
using EMS.Core.Repository.GenericRepository;
using EMS.Core.Services.Master;
using EMS.Core.ViewModelEntitity.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Infrastructure.Services.MasterImplementation
{
    public class MasterService : IMasterService
    {
        private readonly IGenericRepository<SeasonModel, int> _ISeasonRepository;
        private readonly IGenericRepository<RegionMaster, int> _IRegionMasterRepository;

        public MasterService(IGenericRepository<SeasonModel, int> seasonRepository,
            IGenericRepository<RegionMaster, int> regionMasterRepository)
        {
            _ISeasonRepository = seasonRepository;
            _IRegionMasterRepository = regionMasterRepository;

        }
        public async Task<List<RegionMasterVm>> GetRegionMasterList()
        {
            var regionModels = await _IRegionMasterRepository.GetList(x => !x.IsDeleted && x.IsActive);
            var seasonModels = await _ISeasonRepository.GetList(x => x.IsActive && !x.IsDeleted);

            var responseModels = (from rm in regionModels
                                  join sm in seasonModels
                                  on rm.SeasonId equals sm.Id
                                  select new RegionMasterVm
                                  {
                                      Id = rm.Id,
                                      SeasonName = sm.SeasonName,
                                      AreaName = rm.AreaName,
                                      RegionStartDate = rm.RegionStartDate,
                                      ZoneEndDate = rm.ZoneEndDate,
                                      ZoneStartDate = rm.ZoneStartDate,
                                      EndTimeZone = rm.EndTimeZone,
                                      AreaDescription = rm.AreaDescription,
                                      AreaCapacity = rm.AreaCapacity,
                                      AreaManagerEmail = rm.AreaManagerEmail,
                                      ExpectedAttende = rm.ExpectedAttende,
                                      Space = rm.Space,
                                      TargetGroup = rm.TargetGroup,
                                      AreaManagerName = rm.AreaManagerName,
                                      DistrictManagerMobile = rm.DistrictManagerMobile,
                                      Sex = rm.Sex
                                  }).ToList();

            return responseModels;
        }
    }
}
