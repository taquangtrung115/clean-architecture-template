using CleanArchitectureTemplate.Application.Features.Dishes.Dtos;
using MediatR;

namespace CleanArchitectureTemplate.Application.Features.Dishes.Queries.GetDishesForRestaurant;
/*
public class GetDishesForRestaurantQuery : IRequest<IEnumerable<DishDto>>
{
    public int RestaurantId { get; set; }
}*/

// hãy dùng cách này để khởi tạo đối tượng và thuộc tính RestaurantId chỉ có thể Get đáp ứng tính bất biến (Immutable)
public class GetDishesForRestaurantQuery(int restaurantId) : IRequest<IEnumerable<DishDto>>
{
    public int RestaurantId { get; } = restaurantId;
}
