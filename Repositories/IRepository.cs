using Microsoft.EntityFrameworkCore.Storage;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP6.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task<T> GetByIdAsync(params object[] keyValues);
        Task<IEnumerable<T>> GetAllAsync();
        Task AddAsync(T entity);
        void Update(T entity);
        Task SaveAsync();
        Task<IDbContextTransaction> BeginTransactionAsync();
    }
}