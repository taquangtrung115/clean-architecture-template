using CleanArchitectureTemplate.Application.DTO.User.Login;
using CleanArchitectureTemplate.Application.Features.Users.Request.Command;
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

namespace CleanArchitectureTemplate.Application.Features.Users.Handler.Command;
public class LoginCommandHandler(IConfiguration configuration
        , UserManager<ApplicationUser> userManager
        , SignInManager<ApplicationUser> signInManager
        ) : IRequestHandler<LoginCommand, LoginResponse>
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
    public async Task<LoginResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var res = new LoginResponse();
        var result = await signInManager.PasswordSignInAsync(request.Username, request.Password, false, false);
        if (result.Succeeded)
        {
            var user = await userManager.FindByNameAsync(request.Username);
            var token = GenerateJwtToken(user);
            if (Guid.TryParse(user.Id, out Guid userID))
            {
                res.Id = userID;
                res.IsSuccess = true;
                res.Token = token;
                return res;
            }
        }
        res.Errors = new List<string> { "Invalid login attempt." };
        return res;
    }
}
