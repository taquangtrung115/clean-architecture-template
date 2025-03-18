using CleanArchitectureTemplate.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureTemplate.Application.Features.Users.Commands.Login
{
    public class LoginCommandHandler(IConfiguration configuration
        , UserManager<ApplicationUser> userManager
        , SignInManager<ApplicationUser> signInManager
        ) : IRequestHandler<LoginCommand, string>
    {
        private string GenerateJwtToken(ApplicationUser user)
        {
            var key = Encoding.ASCII.GetBytes(configuration["Jwt:Key"]);
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            }),
                Expires = DateTime.UtcNow.AddHours(1),
                Issuer = configuration["Jwt:Issuer"],
                Audience = configuration["Jwt:Audience"],
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        public async Task<string> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var result = await signInManager.PasswordSignInAsync(request.Username, request.Password, false, false);
            if (result.Succeeded)
            {
                var user = await userManager.FindByNameAsync(request.Username);
                var token = GenerateJwtToken(user);
                return token;
            }

            return string.Empty;
        }
    }
}
