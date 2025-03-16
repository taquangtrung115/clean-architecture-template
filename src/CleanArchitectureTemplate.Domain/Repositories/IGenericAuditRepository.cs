namespace CleanArchitectureTemplate.Domain.Repositories;

public interface IGenericAuditRepository<T> : IGenericRepository<T> where T : class
{
    
}