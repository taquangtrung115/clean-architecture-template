using CleanArchitectureTemplate.Application.Features.Dishes.Commands.CreateDish;
using CleanArchitectureTemplate.Application.Features.Dishes.Commands.DeleteDishes;
using CleanArchitectureTemplate.Application.Features.Dishes.Dtos;
using CleanArchitectureTemplate.Application.Features.Dishes.Queries.GetDishesByIdForRestaurant;
using CleanArchitectureTemplate.Application.Features.Dishes.Queries.GetDishesForRestaurant;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitectureTemplate.API.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/restaurants/{restaurantId}/dishes")]
//[Authorize]
public class DishesController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateDish([FromRoute]int restaurantId, CreateDishCommand command)
    {
        command.RestaurantId = restaurantId;

        var dishId = await mediator.Send(command);
        return CreatedAtAction(nameof(GetByIdForRestaurant), new { restaurantId, dishId }, null);
    }

    [HttpGet]
    //[Authorize(Policy = PolicyNames.AtLeast20)]
    public async Task<ActionResult<IEnumerable<DishDto>>> GetAllForRestaurant([FromRoute] int restaurantId)
    {
        var dishes = await mediator.Send(new GetDishesForRestaurantQuery(restaurantId));
        return Ok(dishes);
    }

    [HttpGet("{dishId}")]
    public async Task<ActionResult<DishDto>> GetByIdForRestaurant([FromRoute] int restaurantId, [FromRoute]int dishId)
    {
        var dish = await mediator.Send(new GetDishesByIdForRestaurantQuery(restaurantId, dishId));
        return Ok(dish);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteDishesForRestaurant([FromRoute] int restaurantId)
    {
        await mediator.Send(new DeleteDishesForRestaurantCommand(restaurantId));
        return NoContent();
    }
}