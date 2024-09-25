
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using KUHr.DataAccess.Repository;

namespace KUHr.DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly DbContext _context;

        public UnitOfWork(DbContext context, IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _context = context;
        }

        public IRepositoryAsync<T> GetRepository<T>() where T : class
        {
            return _serviceProvider.GetRequiredService<IRepositoryAsync<T>>();
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!disposing) return;
            _context?.Dispose();
        }
        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
       
        public async Task<int> ExecuteQuery(string sql)
        {
            return await _context.Database.ExecuteSqlRawAsync(sql); 
        }


    }
}
