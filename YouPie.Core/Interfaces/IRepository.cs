using YouPie.Core.Models;

namespace YouPie.Core.Interfaces;

public interface IRepository < T > where T: BaseEntity
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T?> GetAsync(string id);
    Task CreateAsync(T entity);
    Task CreateManyAsync(IEnumerable<T> entities);
    Task ReplaceAsync(string id, T entity);
    Task DeleteAsync(string id);
    Task DeleteAllAsync();
}