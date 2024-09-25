
using KUHr.DataAccess.Repository;
using System;
using System.Threading.Tasks;

namespace KUHr.DataAccess
{
    public interface IUnitOfWork : IDisposable
    {
        IRepositoryAsync<T> GetRepository<T>() where T : class;
        Task<int> SaveChangesAsync();
        Task<int> ExecuteQuery(string sql);
    }
}