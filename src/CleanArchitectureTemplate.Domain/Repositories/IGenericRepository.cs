using System.Linq.Expressions;
using CleanArchitectureTemplate.Domain.Constants;

namespace CleanArchitectureTemplate.Domain.Repositories;

public interface IGenericRepository<T> where T : class
{
    Task<List<T>> GetAllAsync();
    Task<T?> GetByIdAsync(object id);
    Task<T> AddAsync(T entity);
    Task AddRangeAsync(List<T> entities);
    void Update(T entity);
    void UpdateRange(List<T> entities);
    void SoftRemove(T entity);
    void SoftRemoveRange(List<T> entities);
    void Remove(T entity);
    void RemoveRange(List<T> entities);
    void RemoveRange(List<Guid> entities);
    Task<(List<T>, int)> GetAllMatchingAsync(string? searchPhrase, 
        int pageSize, 
        int pageNumber, 
        string? sortBy, 
        SortDirection sortDirection,
        params Expression<Func<T, object>>[] includeProperties);
}