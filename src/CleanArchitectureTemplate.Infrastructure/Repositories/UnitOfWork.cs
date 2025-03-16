using CleanArchitectureTemplate.Domain.Interfaces;
using CleanArchitectureTemplate.Domain.Repositories;
using CleanArchitectureTemplate.Infrastructure.Persistence;

namespace CleanArchitectureTemplate.Infrastructure.Repositories;

public class UnitOfWork(ApplicationDbContext dbContext) : IUnitOfWork
{
    private IRestaurantsRepository? _restaurantsRepository;   // ? ở sau kiểu dữ liệu để compiler hiểu rằng biến khai báo có thể null 
    private IDishesRepository? _dishesRepository;

    // nếu _restaurantsRepository mà null thì _restaurantsRepository = new RestaurantsRepository(_dbContext)
    // điều này đảm bảo rằng khi khởi tạo mới RestaurantsRepository thì nó sẽ lưu lại vào biết _restaurantsRepository
    // để lần sau gọi đến thì _restaurantsRepository đã có sẵn không phải thự hiện new()
    public IRestaurantsRepository RestaurantsRepository =>                  
        _restaurantsRepository ??= new RestaurantsRepository(dbContext);   
    
    public IDishesRepository DishesRepository =>
        _dishesRepository ??= new DishesRepository(dbContext);

    // Save Change
    public async Task<int> SaveChangeAsync() => await dbContext.SaveChangesAsync();
    
    // hàm để hủy đối tượng _dbContext khi dùng xong 
    public void Dispose()
    {
        dbContext.Dispose();
    }
}