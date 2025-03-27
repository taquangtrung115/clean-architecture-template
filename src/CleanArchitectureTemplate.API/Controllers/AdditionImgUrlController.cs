using MediatR;
using Microsoft.AspNetCore.Mvc;
using CleanArchitectureTemplate.Application.DTO.AdditionImgUrl;
using CleanArchitectureTemplate.Application.Features.AdditionImgUrl.Request.Queries.GetAddtionImgUrl;
using CleanArchitectureTemplate.Application.Features.AdditionImgUrl.Request.Queries.GetAddtionImgUrlById;
using CleanArchitectureTemplate.Application.Features.AdditionImgUrl.Request.Command.DeleteAddtionImgUrl;
using CleanArchitectureTemplate.Application.Features.AdditionImgUrl.Request.Command.CreateOrUpdateAddtionImgUrl;

namespace CleanArchitectureTemplate.API.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/additionImgUrls")]
//[Authorize]
public class AdditionImgUrlController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    //[AllowAnonymous]
    //[Authorize(Policy = PolicyNames.CreatedAtleast2Restaurants)]
    public async Task<ActionResult<IEnumerable<AdditionImgUrlDTO>>> GetAll(AdditionImgUrlQueryRequest query)
    {
        var additionImgUrls = await mediator.Send(query);
        return Ok(additionImgUrls);
    }

    [HttpGet("{id}")]
    //[Authorize(Policy = PolicyNames.HasNationality)]
    public async Task<ActionResult<AdditionImgUrlDTO?>> GetById(Guid id)
    {
        var additionImgUrl = await mediator.Send(new AdditionImgUrlQueryByIdRequest { ID = id});
        return Ok(additionImgUrl);
    }

    [HttpPost]
    //[Authorize(Roles = UserRoles.Owner)]
    public async Task<IActionResult> CreateAdditionImgUrl(AdditionImgUrlCommandRequest command)
    {
        var id = await mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id }, null);
    }

    [HttpDelete("{id}")]
    //[Authorize(Roles = UserRoles.Owner)]
    public async Task<IActionResult> DeleteAdditionImgUrl(List<Guid> lstID)
    {
        await mediator.Send(new DeleteAddtionImgUrlCommandRequest { ListID = lstID });
        return NoContent();
    }
}
