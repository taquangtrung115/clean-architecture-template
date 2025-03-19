using CleanArchitectureTemplate.Application.DTO.BaseDTO;
using CleanArchitectureTemplate.Application.DTO.Profile;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureTemplate.Application.Features.Profile.Request.Command
{
    public class CreateProfileCommand : IRequest<BaseCommandResponse<Guid>>
    {
        public ProfileDTO ProfileCreate { get; set; }
    }
}
