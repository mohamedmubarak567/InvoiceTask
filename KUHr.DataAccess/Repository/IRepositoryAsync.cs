using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Query;

namespace KUHr.DataAccess.Repository
{
    public interface IRepositoryAsync<T> where T : class
    {
        Task<IEnumerable<T>> FindAsyncInclude(Expression<Func<T, bool>> predicate = null,
     Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
     Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
     bool disableTracking = true);
        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate,
            Func<IQueryable<T>,
             IIncludableQueryable<T, object>> include = null,
            bool disableTracking = false, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, bool ignoreQueryFilter = false);
        Task RemoveRangeAsync(IEnumerable<T> entities, bool? contextSave = true);
        Task RemoveAsync(T entities, bool? contextSave = true);
        Task UpdateAsync(T entityNew, T entityOld, bool? contextSave = true);
        Task AddRangeAsync(IEnumerable<T> entities, bool? contextSave = true);
        Task<T> AddAsync(T entity, bool? auditSave = true, bool? contextSave = true);
        Task<bool> IsExistAnyAsync(Expression<Func<T, bool>> predicate);
    }
}
