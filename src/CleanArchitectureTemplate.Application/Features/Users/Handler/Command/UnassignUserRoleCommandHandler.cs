using CleanArchitectureTemplate.Application.Features.Users.Request.Command;
using CleanArchitectureTemplate.Domain.Entities;
using CleanArchitectureTemplate.Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureTemplate.Application.Features.Users.Handler.Command;

public class UnassignUserRoleCommandHandler(ILogger<UnassignUserRoleCommandHandler> logger,
    UserManager<ApplicationUser> userManager,
    RoleManager<IdentityRole> roleManager) : IRequestHandler<UnassignUserRoleCommand>
{
    public async Task Handle(UnassignUserRoleCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Unassigning user role: {@Request}", request);
        var user = await userManager.FindByEmailAsync(request.UserEmail)
                   ?? throw new NotFoundException(nameof(ApplicationUser), request.UserEmail);

        var role = await roleManager.FindByNameAsync(request.RoleName)
                   ?? throw new NotFoundException(nameof(IdentityRole), request.RoleName);

        await userManager.RemoveFromRoleAsync(user, role.Name!);

    }
}
