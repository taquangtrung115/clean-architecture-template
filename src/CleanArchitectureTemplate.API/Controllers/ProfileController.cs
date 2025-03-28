using CleanArchitectureTemplate.Application.DTO.Profile;
using CleanArchitectureTemplate.Application.Features.Profile.Request.Command;
using CleanArchitectureTemplate.Application.Features.Profile.Request.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitectureTemplate.API.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/profiles")]
//[Authorize]
public class ProfileController(IMediator mediator) : ControllerBase
{
    //[HttpGet]
    ////[AllowAnonymous]
    ////[Authorize(Policy = PolicyNames.CreatedAtleast2Restaurants)]
    //public async Task<ActionResult<IEnumerable<ProfileDTO>>> GetAll([FromQuery] GetRestaurantsQuery query)
    //{
    //    var restaurants = await mediator.Send(query);
    //    return Ok(restaurants);
    //}

    [HttpGet("getById{id}")]
    //[Authorize(Policy = PolicyNames.HasNationality)]
    public async Task<ActionResult<ProfileDTO?>> GetById([FromRoute] Guid id)
    {
        var res = await mediator.Send(new GetProfileByIDQueryRequest { Id = id });
        return Ok(res);
    }

    //[HttpPost]
    ////[Authorize(Roles = UserRoles.Owner)]
    //public async Task<IActionResult> CreateProfile(CreateProfileCommand command)
    //{
    //    var id = await mediator.Send(command);
    //    return Created();
    //}
}