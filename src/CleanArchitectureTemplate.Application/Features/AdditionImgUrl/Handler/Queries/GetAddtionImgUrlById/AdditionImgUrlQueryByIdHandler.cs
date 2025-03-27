using CleanArchitectureTemplate.Application.DTO.AdditionImgUrl;
using CleanArchitectureTemplate.Application.Features.AdditionImgUrl.Handler.Queries.GetAddtionImgUrl;
using CleanArchitectureTemplate.Application.Features.AdditionImgUrl.Request.Queries.GetAddtionImgUrlById;
using CleanArchitectureTemplate.Domain.Interfaces;
using MapsterMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureTemplate.Application.Features.AdditionImgUrl.Handler.Queries.GetAddtionImgUrlById;

public class AdditionImgUrlQueryHandler(ILogger<AdditionImgUrlQueryHandler> logger,
    IMapper mapper,
    IUnitOfWork unitOfWork) : IRequestHandler<AdditionImgUrlQueryByIdRequest, AdditionImgUrlDTO>
{
    public async Task<AdditionImgUrlDTO> Handle(AdditionImgUrlQueryByIdRequest request, CancellationToken cancellationToken)
    {
        return mapper.Map<AdditionImgUrlDTO>(await unitOfWork.AdditionImgUrlReponsitory.GetByIdAsync(request.ID));
    }
}
