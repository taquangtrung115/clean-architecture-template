using MediatR;

namespace CleanArchitectureTemplate.Application.Features.Dishes.Commands.DeleteDishes;

public class DeleteDishesForRestaurantCommand(int restaurantId) : IRequest
{
    public int RestaurantId { get; } = restaurantId;
}