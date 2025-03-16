using CleanArchitectureTemplate.Application.Features.Dishes.Dtos;
using MediatR;

namespace CleanArchitectureTemplate.Application.Features.Dishes.Queries.GetDishesByIdForRestaurant;

public class GetDishesByIdForRestaurantQuery(int restaurantId, int dishId) : IRequest<DishDto>
{
    public int RestaurantId { get; } = restaurantId;
    public int DishId { get; } = dishId;
}