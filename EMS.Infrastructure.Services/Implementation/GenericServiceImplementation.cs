using EMS.Core.Repository.GenericRepository;
using EMS.Core.Services.GenericService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Infrastructure.Services.Implementation
{
    public class GenericServiceImplementation<TEntity, T> : IGenericService<TEntity, T> where TEntity : class
    {
        private readonly IGenericRepository<TEntity, T> _IGenericRepository;

        public GenericServiceImplementation(IGenericRepository<TEntity, T> genericRepository)
        {
            _IGenericRepository = genericRepository;
        }
        public async Task<bool> CreateEntities(params TEntity[] items)
        {
            return await _IGenericRepository.CreateEntities(items);
        }

        public async Task<bool> CreateEntity(TEntity model)
        {
            return await _IGenericRepository.CreateEntity(model);
        }

        public Task<bool> CreateNewContext()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteEntities(params TEntity[] items)
        {
            return await _IGenericRepository.DeleteEntities(items);
        }

        public async Task<bool> DeleteEntity(TEntity model)
        {
            return await _IGenericRepository.DeleteEntity(model);
        }

        public async Task<IList<TEntity>> GetAll(params Expression<Func<TEntity, object>>[] navigationProperties)
        {
            return await _IGenericRepository.GetAll(navigationProperties);
        }

        public async Task<IList<TEntity>> GetList(Func<TEntity, bool> where, params Expression<Func<TEntity, object>>[] navigationProperties)
        {
            return await _IGenericRepository.GetList(where, navigationProperties);
        }

        public async Task<TEntity> GetSingle(Func<TEntity, bool> where)
        {
            return await _IGenericRepository.GetSingle(where);
        }

        public async Task<bool> UpdateEntities(params TEntity[] items)
        {
            return await _IGenericRepository.UpdateEntities(items);
        }

        public async Task<bool> UpdateEntity(TEntity model)
        {
            return await _IGenericRepository.UpdateEntity(model);
        }
    }
}
