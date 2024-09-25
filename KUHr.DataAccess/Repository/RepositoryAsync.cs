using LinqKit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace KUHr.DataAccess.Repository
{
    public class RepositoryAsync<T> : IRepositoryAsync<T> where T : class
    {
        protected readonly DbContext Context;
        protected readonly DbSet<T> DbSet;

        public RepositoryAsync(DbContext context)
        {
            Context = context;
            DbSet = Context.Set<T>();
        }

        public async Task<IEnumerable<T>> FindAsyncInclude(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, bool disableTracking = true)
        {
            IQueryable<T> query = DbSet;
            if (disableTracking)
            {
                query = query.AsNoTracking();
            }
            if (predicate != null)
            {
                query = query.Where(predicate);
            }
            if (orderBy != null)
            {
                query = orderBy(query);
            }
            if (include != null)
            {
                query = include(query).AsSplitQuery();
            }
            return await query.ToListAsync();
        }

        public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, bool disableTracking = false, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, bool ignoreQueryFilter = false)
        {
            IQueryable<T> query = DbSet;
            if (disableTracking) query = query.AsNoTracking();
            if (ignoreQueryFilter) query = query.IgnoreQueryFilters();
            if (include != null) query = include(query).AsSplitQuery();
            if (orderBy != null) query = orderBy(query);
            return await query.FirstOrDefaultAsync(predicate);
        }
        public async Task RemoveRangeAsync(IEnumerable<T> entities, bool? contextSave = true)
        {
            var task = Task.Factory.StartNew(() =>
            {
                if (contextSave == true)
            {
                DbSet.RemoveRange(entities.ToList());
            }
            });
            await task;
        }
        public async Task RemoveAsync(T entities, bool? contextSave = true)
        {
            var task = Task.Factory.StartNew(() =>
            {
                if (contextSave == true)
                {
                    DbSet.Remove(entities);
                }
            });
            await task;
        }
        public async Task UpdateAsync(T entityNew, T entityOld, bool? contextSave = true)
        {
            var task = Task.Factory.StartNew(() =>
            {
                         
                if (contextSave != true) return;
                Context.Entry(entityOld).State = EntityState.Detached; // exception when trying to change the state
                Context.Entry(entityNew).State = EntityState.Modified; // exception when trying to change the state
                Context.Entry(entityOld).CurrentValues.SetValues(entityNew);
            });
            await task;
        }
        public async Task AddRangeAsync(IEnumerable<T> entities, bool? contextSave = true)
        {
            var baseEntityModels = entities.ToList();
            if (contextSave == true)
            {
                await DbSet.AddRangeAsync(baseEntityModels);
            }
        }
        private Expression<Func<T, bool>> GetPredicate(Expression<Func<T, bool>> predicate = null)
        {
            ExpressionStarter<T> expr;
            if (predicate == null) expr = PredicateBuilder.New<T>(true); else expr = predicate;
            return expr;
        }
        public async Task<T> AddAsync(T entity, bool? auditSave = true, bool? contextSave = true)
        {
            return contextSave == true ? (await DbSet.AddAsync(entity)).Entity : entity;
        }
        public async Task<bool> IsExistAnyAsync(Expression<Func<T, bool>> predicate) => await DbSet.AnyAsync(GetPredicate(predicate));
    }
    }
