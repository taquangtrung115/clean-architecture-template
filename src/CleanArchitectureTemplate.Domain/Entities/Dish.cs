using CleanArchitectureTemplate.Domain.Entities.Base;

namespace CleanArchitectureTemplate.Domain.Entities;

public class Dish : Entity<int>
{
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public decimal Price { get; set; }

    public int? KiloCalories { get; set; }

    public int RestaurantId { get; set; }
}
