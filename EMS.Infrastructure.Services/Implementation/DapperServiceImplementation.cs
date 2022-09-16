using EMS.Core.Repository.GenericRepository;
using EMS.Core.Services.GenericService;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Infrastructure.Services.Implementation
{
    public class DapperServiceImplementation<TParams> : IDapperService<TParams>
    {
        private readonly IDapperRepository<TParams> _IDapperRepository;

        public DapperServiceImplementation(IDapperRepository<TParams> dapperRepository) => _IDapperRepository = dapperRepository;
        public void Dispose() => _IDapperRepository.Dispose();
        public TModel Execute<TModel>(string sp, TParams entity) => _IDapperRepository.Execute<TModel>(sp, entity);
        public TModel Get<TModel>(string sp, TParams entity) => _IDapperRepository.Get<TModel>(sp, entity);
        public IEnumerable<TModel> GetAll<TModel>(string sp, TParams entity) => _IDapperRepository.GetAll<TModel>(sp, entity);
        public DbConnection GetDbconnection() => _IDapperRepository.GetDbconnection();

    }
}
