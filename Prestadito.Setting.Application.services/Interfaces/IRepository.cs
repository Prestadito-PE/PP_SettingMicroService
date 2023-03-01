using System.Linq.Expressions;

namespace Prestadito.Setting.Application.Services.Interfaces
{
    public interface IRepository<T>
    {
        ValueTask<List<T>> GetAllAsync(Expression<Func<T, bool>> filter);
        ValueTask<T> GetAsync(Expression<Func<T, bool>> filter);
        ValueTask<T> InsertOneAsync(T entity);
        ValueTask<bool> UpdateOneAsync(T entity);
        ValueTask<bool> DeleteOneAsync(Expression<Func<T, bool>> filter);
    }
}
