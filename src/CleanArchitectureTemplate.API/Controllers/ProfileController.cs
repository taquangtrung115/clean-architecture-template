using CleanArchitectureTemplate.Application.DTO.Profile;
using CleanArchitectureTemplate.Application.Features.Restaurants.Commands.CreateRestaurant;
using CleanArchitectureTemplate.Application.Features.Restaurants.Queries.GetAllRestaurants;
using CleanArchitectureTemplate.Application.Features.Restaurants.Queries.GetRestaurantById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitectureTemplate.API.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/profile")]
//[Authorize]
public class ProfileController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    //[AllowAnonymous]
    //[Authorize(Policy = PolicyNames.CreatedAtleast2Restaurants)]
    public async Task<ActionResult<IEnumerable<ProfileDTO>>> GetAll([FromQuery] GetRestaurantsQuery query)
    {
        var restaurants = await mediator.Send(query);
        return Ok(restaurants);
    }

    [HttpGet("{id}")]
    //[Authorize(Policy = PolicyNames.HasNationality)]
    public async Task<ActionResult<ProfileDTO?>> GetById([FromRoute] int id)
    {
        var restaurant = await mediator.Send(new GetRestaurantByIdQuery(id));
        return Ok(restaurant);
    }

    [HttpPost]
    //[Authorize(Roles = UserRoles.Owner)]
    public async Task<IActionResult> CreateProfile(CreateRestaurantCommand command)
    {
        var id = await mediator.Send(command);
        return Created();
    }
}