
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitectureTemplate.API.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/categories")]
//[Authorize]
public class CategoryController(IMediator mediator) : ControllerBase
{
    
    //[HttpGet]
    ////[AllowAnonymous]
    ////[Authorize(Policy = PolicyNames.CreatedAtleast2Restaurants)]
    //public async Task<ActionResult<IEnumerable<RestaurantDto>>> GetAll([FromQuery] GetRestaurantsQuery query)
    //{
    //    var restaurants = await mediator.Send(query);
    //    return Ok(restaurants);
    //}
    
    //[HttpGet("{id}")]
    ////[Authorize(Policy = PolicyNames.HasNationality)]
    //public async Task<ActionResult<RestaurantDto?>> GetById([FromRoute]int id)
    //{
    //    var restaurant = await mediator.Send(new GetRestaurantByIdQuery(id));
    //    return Ok(restaurant);
    //}
    
    //[HttpPost]
    ////[Authorize(Roles = UserRoles.Owner)]
    //public async Task<IActionResult> CreateRestaurant(CreateRestaurantCommand command)
    //{
    //    var id = await mediator.Send(command);
    //    return Created();
    //}
    
}
