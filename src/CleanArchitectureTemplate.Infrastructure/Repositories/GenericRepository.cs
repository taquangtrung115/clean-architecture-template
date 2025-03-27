using System.Linq.Expressions;
using CleanArchitectureTemplate.Domain.Constants;
using CleanArchitectureTemplate.Domain.Repositories;
using CleanArchitectureTemplate.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitectureTemplate.Infrastructure.Repositories;

public abstract class GenericRepository<T>(ApplicationDbContext dbContext) : IGenericRepository<T>
    where T : class
{
    protected readonly DbSet<T> DbSet = dbContext.Set<T>();

    public Task<List<T>> GetAllAsync() => DbSet.ToListAsync();

    public async Task<T?> GetByIdAsync(object id)
    {
        return await DbSet.FindAsync(id);
    }

    public async Task<T> AddAsync(T entity)
    {
        await DbSet.AddAsync(entity);
        return entity;
    } 

    public async Task AddRangeAsync(List<T> entities)
    {
        await DbSet.AddRangeAsync(entities);
    }
    
    public void Update(T entity)
    {
        DbSet.Update(entity);
    }

    public void UpdateRange(List<T> entities)
    {
        DbSet.UpdateRange();
    }

    public void SoftRemove(T entity)
    {
        throw new NotImplementedException();  
    }

    public void SoftRemoveRange(List<T> entities)
    {
        throw new NotImplementedException();
    }

    public void Remove(T entity)
    {
        DbSet.Remove(entity);
    }

    public void RemoveRange(List<T> entities)
    {
        DbSet.RemoveRange(entities);
    }
    public void RemoveRange(List<Guid> ids)
    {
        var entities = DbSet.Where(e => ids.Contains((Guid)(object)e.GetType().GetProperty("Id").GetValue(e, null))).ToList();
        DbSet.RemoveRange(entities);
    }

    // khai báo các phương thức abstract để các lớp kế thừa phải override 
    public abstract Task<(List<T>, int)> GetAllMatchingAsync(string? searchPhrase,
        int pageSize,
        int pageNumber,
        string? sortBy,
        SortDirection sortDirection,
        params Expression<Func<T, object>>[] includeProperties);
}