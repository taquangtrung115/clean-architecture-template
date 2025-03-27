using CleanArchitectureTemplate.Application.DTO.BaseDTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureTemplate.Application.Features.AdditionImgUrl.Request.Command.DeleteAddtionImgUrl;

public class DeleteAddtionImgUrlCommandRequest : IRequest<BaseCommandResponse<Guid>>
{
    public List<Guid> ListID { get; set; }
}
