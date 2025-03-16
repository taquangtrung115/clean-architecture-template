using CleanArchitectureTemplate.Application.Features.Restaurants.Dtos;
using MediatR;

namespace CleanArchitectureTemplate.Application.Features.Restaurants.Queries.GetRestaurantById;

public class GetRestaurantByIdQuery(int id) : IRequest<RestaurantDto>
{
    public int Id { get; } = id;
}