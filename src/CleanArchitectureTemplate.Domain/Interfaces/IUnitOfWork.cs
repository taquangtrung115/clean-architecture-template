using CleanArchitectureTemplate.Domain.Repositories;

namespace CleanArchitectureTemplate.Domain.Interfaces;

public interface IUnitOfWork : IDisposable
{
    public IProfileRepository ProfileRepository { get; }
    public IAdditionImgUrlReponsitory AdditionImgUrlReponsitory { get; }
    public IAddressReponsitory AddressReponsitory { get; }
    public IBrandReponsitory BrandReponsitory { get; }
    public ICategoryReponsitory CategoryReponsitory { get; }
    public IOrderDetailReponsitory OrderDetailReponsitory { get; }
    public IOrderReponsitory OrderReponsitory { get; }
    public IProductReponsitory ProductReponsitory { get; }
    public IReviewReponsitory ReviewReponsitory { get; }

    public Task<int> SaveChangeAsync();
    public void ApplyAuditInformation();
}