using EMS.Core.Entities.Common;
using EMS.Core.Repository.GenericRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Infrastructure.Repository.Implementation
{
    public class GenericImplementation<TEntity, T> : IGenericRepository<TEntity, T> where TEntity : class
    {
        private readonly EMSContext baseContext = null;
        private readonly DbSet<TEntity> TEntities = null;

        public GenericImplementation()
        {
            baseContext = new EMSContext();
            TEntities = baseContext.Set<TEntity>();
        }
        public async Task<bool> CreateEntities(params TEntity[] items)
        {
            try
            {
                await TEntities.AddRangeAsync(items);
                await baseContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> CreateEntity(TEntity model)
        {
            try
            {
                await TEntities.AddAsync(model);
                await baseContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public Task<bool> CreateNewContext()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteEntities(params TEntity[] items)
        {
            try
            {
                baseContext.UpdateRange(items);
                await baseContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> DeleteEntity(TEntity model)
        {
            try
            {
                baseContext.UpdateRange(model);
                await baseContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<IList<TEntity>> GetAll(params Expression<Func<TEntity, object>>[] navigationProperties)
        {
            List<TEntity> tList;
            IQueryable<TEntity> dbQuery = baseContext.Set<TEntity>();
            foreach (Expression<Func<TEntity, object>> navigationProp in navigationProperties)
            {
                dbQuery.Include<TEntity, object>(navigationProp);
            }
            tList = await dbQuery.AsNoTracking().ToListAsync<TEntity>();
            return tList;
        }

        public async Task<IList<TEntity>> GetList(Func<TEntity, bool> where, params Expression<Func<TEntity, object>>[] navigationProperties)
        {
            List<TEntity> list = new List<TEntity>();
            try
            {
                IQueryable<TEntity> dbQuery = baseContext.Set<TEntity>();

                //Apply eager loading
                foreach (Expression<Func<TEntity, object>> navigationProperty in navigationProperties)
                {
                    dbQuery = dbQuery.Include<TEntity, object>(navigationProperty);
                }

                list = dbQuery.AsNoTracking().Where(where).ToList<TEntity>();
                return await Task.Run(() => list);
            }
            catch (Exception ex)
            {
                return await Task.Run(() => list);
            }
        }

        public async Task<TEntity> GetSingle(Func<TEntity, bool> where)
        {
            TEntity item = null;
            IQueryable<TEntity> dbQuery = baseContext.Set<TEntity>();

            item = dbQuery
                .AsNoTracking() //Don't track any changes for the selected item
                .FirstOrDefault(where); //Apply where clause
            return await Task.Run(() => item);
        }

        public async Task<bool> UpdateEntities(params TEntity[] items)
        {
            try
            {
                baseContext.UpdateRange(items);
                await baseContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> UpdateEntity(TEntity model)
        {
            try
            {
                baseContext.Update(model);
                await baseContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
