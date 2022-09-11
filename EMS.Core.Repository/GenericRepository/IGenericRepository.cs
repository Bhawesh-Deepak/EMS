using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Core.Repository.GenericRepository
{
    public interface IGenericRepository<TEntity, T> where TEntity:class
    {
        Task<IList<TEntity>> GetAll(params Expression<Func<TEntity, object>>[] navigationProperties);
        Task<IList<TEntity>> GetList(Func<TEntity, bool> where, params Expression<Func<TEntity, object>>[] navigationProperties);
        Task<TEntity> GetSingle(Func<TEntity, bool> where);
        Task<bool> CreateEntities(params TEntity[] items);
        Task<bool> UpdateEntities(params TEntity[] items);
        Task<bool> DeleteEntities(params TEntity[] items);
        Task<bool> CreateEntity(TEntity model);
        Task<bool> UpdateEntity(TEntity model);
        Task<bool> DeleteEntity(TEntity model);
        Task<bool> CreateNewContext();
    }
}
