using MediatR;

namespace CleanArchitectureTemplate.Application.Features.Restaurants.Commands.DeleteRestaurant;

public class DeleteRestaurantCommand(int id) : IRequest
{
    public int Id { get; } = id;
}