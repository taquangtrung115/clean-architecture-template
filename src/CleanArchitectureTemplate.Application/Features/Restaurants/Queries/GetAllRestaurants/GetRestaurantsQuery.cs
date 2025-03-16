using CleanArchitectureTemplate.Application.Common;
using CleanArchitectureTemplate.Application.Features.Restaurants.Dtos;
using CleanArchitectureTemplate.Domain.Constants;
using MediatR;

namespace CleanArchitectureTemplate.Application.Features.Restaurants.Queries.GetAllRestaurants;

public class GetRestaurantsQuery : IRequest<PageResult<RestaurantDto>>
{
    public string? SearchPhrase { get; set; }
    public int PageNumber { get; set; } 
    public int PageSize { get; set; } 
    public string? SortBy { get; set; }
    public SortDirection SortDirection { get; set; }
}