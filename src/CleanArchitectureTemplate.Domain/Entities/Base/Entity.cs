namespace CleanArchitectureTemplate.Domain.Entities.Base;

public abstract class Entity<TKey>
{
    public TKey Id { get; set; }
}