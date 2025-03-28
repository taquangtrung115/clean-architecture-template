using CleanArchitectureTemplate.Application.DTO.Profile;
using CleanArchitectureTemplate.Application.Features.Profile.Request.Command;
using CleanArchitectureTemplate.Application.Features.Users.Request.Command;
using CleanArchitectureTemplate.Domain.Entities;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureTemplate.Application.Features.Users.Handler.Command;
public class RegisterCommandHandler(IMediator mediator, IMapper mapper, UserManager<ApplicationUser> userManager
        , SignInManager<ApplicationUser> signInManager
        ) : IRequestHandler<RegisterCommand, IdentityResult>
{
    public async Task<IdentityResult> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var user = new ApplicationUser
        {
            UserName = request.Username,
            Email = request.Email,
            DateOfBirth = request.DayOfBirth,
            Id = Guid.NewGuid().ToString()
        };
        var profileCreate = mapper.Map<ProfileDTO>(request);
        if (!Guid.TryParse(user.Id, out Guid userID))
        {
            var error = new IdentityError { Description = "Error getting userID" };
            return IdentityResult.Failed(error);
        }
        profileCreate.UserID = userID;
        var createProfileCommand = new CreateProfileCommand { ProfileCreate = profileCreate };
        var resultProfile = await mediator.Send(createProfileCommand);
        if (resultProfile.Success)
        {
            return await userManager.CreateAsync(user, request.Password);
        }
        
        return new IdentityResult
        {
            Errors = { }
        };
    }
}
