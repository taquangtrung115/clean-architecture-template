using CleanArchitectureTemplate.Application.Common;
using CleanArchitectureTemplate.Application.Features.Restaurants.Dtos;
using CleanArchitectureTemplate.Domain.Interfaces;
using CleanArchitectureTemplate.Domain.Repositories;
using MapsterMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CleanArchitectureTemplate.Application.Features.Restaurants.Queries.GetAllRestaurants;

public class GetRestaurantsQueryHandler(ILogger<GetRestaurantsQueryHandler> logger,
    IMapper mapper,
    IUnitOfWork unitOfWork) : IRequestHandler<GetRestaurantsQuery,PageResult<RestaurantDto>>
{
    public async Task<PageResult<RestaurantDto>> Handle(GetRestaurantsQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting all restaurants");
        var (restaurants, totalCount) = await unitOfWork.RestaurantsRepository
            .GetAllMatchingAsync(request.SearchPhrase,
                request.PageSize,
                request.PageNumber,
                request.SortBy,
                request.SortDirection,
                r => r.Dishes);

        var restaurantsDtos = mapper.Map<IEnumerable<RestaurantDto>>(restaurants);

        var result = new PageResult<RestaurantDto>(restaurantsDtos, totalCount, request.PageSize, request.PageNumber);
        return result;
    }
}