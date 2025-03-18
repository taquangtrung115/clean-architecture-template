using CleanArchitectureTemplate.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureTemplate.Application.Features.Users.Commands.Register
{
    public class RegisterCommandHandler(UserManager<ApplicationUser> userManager
        , SignInManager<ApplicationUser> signInManager
        ) : IRequestHandler<RegisterCommand, IdentityResult>
    {
        public async Task<IdentityResult> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var user = new ApplicationUser
            {
                UserName = request.Username,
                Email = request.Email
            };

            var result = await userManager.CreateAsync(user, request.Password);

            return result;
        }
    }
}
