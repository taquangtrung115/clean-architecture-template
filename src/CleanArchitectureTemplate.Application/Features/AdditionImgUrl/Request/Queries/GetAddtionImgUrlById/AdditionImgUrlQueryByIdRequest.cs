using CleanArchitectureTemplate.Application.DTO.AdditionImgUrl;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureTemplate.Application.Features.AdditionImgUrl.Request.Queries.GetAddtionImgUrlById;

public class AdditionImgUrlQueryByIdRequest : IRequest<AdditionImgUrlDTO>
{
    public Guid ID { get; set; }
}
