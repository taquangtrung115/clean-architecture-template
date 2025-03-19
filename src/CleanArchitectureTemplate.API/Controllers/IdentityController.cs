using Asp.Versioning;
using CleanArchitectureTemplate.Application.Features.Users.Request.Command;
using CleanArchitectureTemplate.Domain.Constants;
using CleanArchitectureTemplate.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CleanArchitectureTemplate.API.Controllers;

[ApiController]
[Route("api/identity")]
[ApiVersionNeutral]
public class IdentityController(IMediator mediator, IConfiguration configuration, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager) : ControllerBase
{
    private readonly IConfiguration _configuration = configuration;
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly SignInManager<ApplicationUser> _signInManager = signInManager;

    [HttpPatch("user")]
    [Authorize]
    public async Task<IActionResult> UpdateUserDetails(UpdateUserDetailsCommand command)
    {
        await mediator.Send(command);
        return NoContent();
    }

    [HttpPost("userRole")]
    [Authorize(Roles = UserRoles.Admin)]
    public async Task<IActionResult> AssignUserRole(AssignUserRoleCommand command)
    {
        await mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("userRole")]
    [Authorize(Roles = UserRoles.Admin)]
    public async Task<IActionResult> UnassignUserRole(UnassignUserRoleCommand command)
    {
        await mediator.Send(command);
        return NoContent();
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login(LoginCommand command)
    {
        var result = await mediator.Send(command);
        if (!string.IsNullOrEmpty(result))
            return Ok(new { Token = result });
        return Unauthorized();
    }
    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<IActionResult> Register(RegisterCommand command)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await mediator.Send(command);
        if (result.Succeeded)
        {
            return Ok(new { message = "User registered successfully" });
        }

        return BadRequest(result.Errors);
    }
}