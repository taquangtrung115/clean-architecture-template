using CleanArchitectureTemplate.Application.DTO.User.Login;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureTemplate.Application.Features.Users.Request.Command;
public class LoginCommand : IRequest<LoginResponse>
{
    public string Username { get; set; }
    public string Password { get; set; }
}
