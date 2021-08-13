using Core.Entities.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Core.DataAccess.RepositoryPattern
{
    public class EntityRepositoryBase<TEntity, TContext> : IEntityRepository<TEntity>
        where TEntity : class, IEntity, new()
        where TContext : DbContext
    {
        protected TContext Context;
        protected DbSet<TEntity> Table;

        public EntityRepositoryBase(TContext context)
        {
            Context = context;
            Table = Context.Set<TEntity>();
        }

        public async Task<bool> AddAsync(TEntity entity)
        {
            Context.Entry<TEntity>(entity).State = EntityState.Added;
            return (await Context.SaveChangesAsync()) > 0;
        }
        public async Task<bool> HardDeleteAsync(TEntity entity)
        {
            Context.Entry<TEntity>(entity).State = EntityState.Deleted;
            return (await Context.SaveChangesAsync()) > 0;
        }

        public async Task<bool> UpdateAsync(TEntity entity)
        {
            Context.Entry<TEntity>(entity).State = EntityState.Modified;
            return (await Context.SaveChangesAsync()) > 0;
        }

        public async Task<bool> AddBulkAsync(List<TEntity> entities)
        {
            await Context.AddRangeAsync(entities);
            return (await Context.SaveChangesAsync()) > 0;
        }

        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, bool ignoreQueryFilters = false, bool disableTracking = false)
        {
            var query = Table.AsQueryable();

            if (disableTracking == true)
                query = query.AsNoTracking();

            if (ignoreQueryFilters)
                query = query.IgnoreQueryFilters();

            if (include != null)
                query = include(query);


            return await query.FirstOrDefaultAsync(predicate);
        }

        public async Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, bool ignoreQueryFilters = false, bool disableTracking = false)
        {
            var query = Table.AsQueryable();

            if (disableTracking == true)
                query = query.AsNoTracking();

            if (ignoreQueryFilters)
                query = query.IgnoreQueryFilters();

            if (include != null)
                query = include(query);

            if (predicate != null)
                query = query.Where(predicate);


            return await query.ToListAsync();
        }

        public async Task<bool> IsExistsAsync(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, bool ignoreQueryFilters = false)
        {
            var query = Table.AsQueryable();

            if (ignoreQueryFilters)
                query = query.IgnoreQueryFilters();

            if (include != null)
                query = include(query);

            return await query.AnyAsync(predicate);
        }

        public async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, bool ignoreQueryFilters = false)
        {
            var query = Table.AsQueryable();

            if (ignoreQueryFilters)
                query = query.IgnoreQueryFilters();

            if (include != null)
                query = include(query);

            if (predicate != null)
                query = query.Where(predicate);

            return await query.CountAsync();
        }
    }
}
