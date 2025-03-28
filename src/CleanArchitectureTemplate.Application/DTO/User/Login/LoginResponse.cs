using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureTemplate.Application.DTO.User.Login;

public class LoginResponse
{
    public Guid Id { get; set; }
    public bool IsSuccess { get; set; } = false;
    public List<string> Errors { get; set; }
    public string Token { get; set; }
}
