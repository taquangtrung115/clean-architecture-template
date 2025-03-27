using CleanArchitectureTemplate.Domain.Interfaces;
using CleanArchitectureTemplate.Domain.Repositories;
using CleanArchitectureTemplate.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using CleanArchitectureTemplate.Domain.Entities.Base;

namespace CleanArchitectureTemplate.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private IProfileRepository? _profileRepository;
    private IAdditionImgUrlReponsitory? _additionImgUrlReponsitory;
    private IAddressReponsitory? _addressReponsitory;
    private IBrandReponsitory? _brandReponsitory;
    private ICategoryReponsitory? _categoryReponsitory;
    private IOrderDetailReponsitory? _orderDetailReponsitory;
    private IOrderReponsitory? _orderReponsitory;
    private IProductReponsitory? _productReponsitory;
    private IReviewReponsitory? _reviewReponsitory;

    public UnitOfWork(ApplicationDbContext dbContext, IHttpContextAccessor httpContextAccessor)
    {
        _dbContext = dbContext;
        _httpContextAccessor = httpContextAccessor;
    }

    public IProfileRepository ProfileRepository =>
         _profileRepository ??= new ProfileRepository(_dbContext); 
    public IAdditionImgUrlReponsitory AdditionImgUrlReponsitory =>
         _additionImgUrlReponsitory ??= new AdditionImgUrlReponsitory(_dbContext); 
    public IAddressReponsitory AddressReponsitory =>
         _addressReponsitory ??= new AddressReponsitory(_dbContext);
    public IBrandReponsitory BrandReponsitory =>
         _brandReponsitory ??= new BrandReponsitory(_dbContext);
    public ICategoryReponsitory CategoryReponsitory =>
         _categoryReponsitory ??= new CategoryReponsitory(_dbContext); 
    public IOrderDetailReponsitory OrderDetailReponsitory =>
         _orderDetailReponsitory ??= new OrderDetailReponsitory(_dbContext);
    public IOrderReponsitory OrderReponsitory =>
         _orderReponsitory ??= new OrderReponsitory(_dbContext); 
    public IProductReponsitory ProductReponsitory =>
         _productReponsitory ??= new ProductReponsitory(_dbContext);
    public IReviewReponsitory ReviewReponsitory =>
         _reviewReponsitory ??= new ReviewReponsitory(_dbContext);

    public async Task<int> SaveChangeAsync<IKey>()
    {
        ApplyAuditInformation<IKey>();
        return await _dbContext.SaveChangesAsync();
    }

    public void Dispose()
    {
        _dbContext.Dispose();
    }

    public void ApplyAuditInformation<IKey>()
    {
        var entries = _dbContext.ChangeTracker.Entries<AuditableEntity<IKey>>();

        string? currentUser = _httpContextAccessor.HttpContext?.User.Identity?.Name ?? "System";

        foreach (var entry in entries)
        {
            try
            {
                // Check if the Id is of type Guid and if it is empty, initialize it with a new Guid
                if (entry.Entity.Id is Guid guid && guid == Guid.Empty)
                {
                    entry.Entity.Id = (IKey)(object)Guid.NewGuid();
                }
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreationDate = DateTime.UtcNow;
                        entry.Entity.CreatedBy = currentUser;
                        entry.Entity.IsDeleted = false;
                        break;

                    case EntityState.Modified:
                        entry.Entity.ModificationDate = DateTime.UtcNow;
                        entry.Entity.ModificationBy = currentUser;
                        break;

                    case EntityState.Deleted:
                        entry.State = EntityState.Modified;
                        entry.Entity.IsDeleted = true;
                        entry.Entity.ModificationDate = DateTime.UtcNow;
                        entry.Entity.ModificationBy = currentUser;
                        break;
                }
            }
            catch (Exception ex)
            {

            }
            
        }
    }
}
