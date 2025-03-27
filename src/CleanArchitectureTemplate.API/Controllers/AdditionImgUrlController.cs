using CleanArchitectureTemplate.Application.Features.AdditionImgUrls.Commands.CreateAdditionImgUrl;
using CleanArchitectureTemplate.Application.Features.AdditionImgUrls.Commands.UpdateAdditionImgUrl;
using CleanArchitectureTemplate.Application.Features.AdditionImgUrls.Commands.DeleteAdditionImgUrl;
using CleanArchitectureTemplate.Application.Features.AdditionImgUrls.Queries.GetAllAdditionImgUrls;
using CleanArchitectureTemplate.Application.Features.AdditionImgUrls.Queries.GetAdditionImgUrlById;
using CleanArchitectureTemplate.Application.Features.AdditionImgUrls.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitectureTemplate.API.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/additionImgUrls")]
//[Authorize]
public class AdditionImgUrlController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    //[AllowAnonymous]
    //[Authorize(Policy = PolicyNames.CreatedAtleast2Restaurants)]
    public async Task<ActionResult<IEnumerable<AdditionImgUrlDto>>> GetAll([FromQuery] GetAllAdditionImgUrlsQuery query)
    {
        var additionImgUrls = await mediator.Send(query);
        return Ok(additionImgUrls);
    }

    [HttpGet("{id}")]
    //[Authorize(Policy = PolicyNames.HasNationality)]
    public async Task<ActionResult<AdditionImgUrlDto?>> GetById([FromRoute] int id)
    {
        var additionImgUrl = await mediator.Send(new GetAdditionImgUrlByIdQuery(id));
        return Ok(additionImgUrl);
    }

    [HttpPost]
    //[Authorize(Roles = UserRoles.Owner)]
    public async Task<IActionResult> CreateAdditionImgUrl(CreateAdditionImgUrlCommand command)
    {
        var id = await mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id }, null);
    }

    [HttpPut("{id}")]
    //[Authorize(Roles = UserRoles.Owner)]
    public async Task<IActionResult> UpdateAdditionImgUrl([FromRoute] int id, UpdateAdditionImgUrlCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest();
        }

        await mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("{id}")]
    //[Authorize(Roles = UserRoles.Owner)]
    public async Task<IActionResult> DeleteAdditionImgUrl([FromRoute] int id)
    {
        await mediator.Send(new DeleteAdditionImgUrlCommand(id));
        return NoContent();
    }
}
