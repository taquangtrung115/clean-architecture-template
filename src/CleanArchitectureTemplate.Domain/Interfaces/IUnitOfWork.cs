using CleanArchitectureTemplate.Domain.Repositories;

namespace CleanArchitectureTemplate.Domain.Interfaces;

public interface IUnitOfWork : IDisposable
{
    public IRestaurantsRepository RestaurantsRepository { get; }
    public IDishesRepository DishesRepository { get; }
    public IProfileRepository ProfileRepository { get; }

    public Task<int> SaveChangeAsync();
}