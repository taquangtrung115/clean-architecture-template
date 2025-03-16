using CleanArchitectureTemplate.Domain.Entities.Base;

namespace CleanArchitectureTemplate.Domain.Entities;

public class Restaurant : AuditableEntity<int>
{
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string Category { get; set; } = default!;
    public bool HasDelivery { get; set; }

    public string? ContactEmail { get; set; }
    public string? ContactNumber { get; set; }

    public Address? Address { get; set; }
    public List<Dish> Dishes { get; set; } = new();

    public ApplicationUser Owner { get; set; } = default!;
    public string OwnerId { get; set; } = default!;
    public string? LogoUrl { get; set; }
}
