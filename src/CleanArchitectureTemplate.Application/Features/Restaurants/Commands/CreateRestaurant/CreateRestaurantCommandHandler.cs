using CleanArchitectureTemplate.Application.ClaimUser;
using CleanArchitectureTemplate.Domain.Entities;
using CleanArchitectureTemplate.Domain.Interfaces;
using CleanArchitectureTemplate.Domain.Repositories;
using MapsterMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CleanArchitectureTemplate.Application.Features.Restaurants.Commands.CreateRestaurant;

public class CreateRestaurantCommandHandler(ILogger<CreateRestaurantCommandHandler> logger,
    IMapper mapper,
    IUnitOfWork unitOfWork,
    IUserContext userContext) : IRequestHandler<CreateRestaurantCommand,int>
{
    public async Task<int> Handle(CreateRestaurantCommand request, CancellationToken cancellationToken)
    {
        var currentUser = userContext.GetCurrentUser();
        
        logger.LogInformation("{UserEmail} [{UserId}] is creating a new restaurant {@Restaurant}", 
            currentUser.Email,
            currentUser.Id,
            request);
        
        var restaurant = mapper.Map<Restaurant>(request);
        restaurant.OwnerId = currentUser.Id;

        var entity = await unitOfWork.RestaurantsRepository.AddAsync(restaurant);
        return entity.Id;
    }
}