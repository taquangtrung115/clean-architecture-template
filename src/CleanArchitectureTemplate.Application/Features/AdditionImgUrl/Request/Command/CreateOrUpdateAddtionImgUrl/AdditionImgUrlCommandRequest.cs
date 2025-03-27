using CleanArchitectureTemplate.Application.DTO.AdditionImgUrl;
using CleanArchitectureTemplate.Application.DTO.BaseDTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureTemplate.Application.Features.AdditionImgUrl.Request.Command.CreateOrUpdateAddtionImgUrl;

public class AdditionImgUrlCommandRequest : IRequest<BaseCommandResponse<Guid>>
{
    public AdditionImgUrlDTO AdditionImgUrlDTO { get; set; }
}
