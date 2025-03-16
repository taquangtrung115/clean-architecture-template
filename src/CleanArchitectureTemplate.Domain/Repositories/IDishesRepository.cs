using CleanArchitectureTemplate.Domain.Entities;

namespace CleanArchitectureTemplate.Domain.Repositories;

public interface IDishesRepository : IGenericRepository<Dish>
{
    /*Task<int> Create(Dish entity);
    Task Delete(IEnumerable<Dish> entities);*/
}
