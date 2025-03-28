using CleanArchitectureTemplate.Application.Common;
using CleanArchitectureTemplate.Application.DTO.AdditionImgUrl;
using CleanArchitectureTemplate.Application.Features.AdditionImgUrl.Request.Queries.GetAddtionImgUrl;
using CleanArchitectureTemplate.Domain.Interfaces;
using MapsterMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureTemplate.Application.Features.AdditionImgUrl.Handler.Queries.GetAddtionImgUrl;

public class AdditionImgUrlQueryHandler(ILogger<AdditionImgUrlQueryHandler> logger,
    IMapper mapper,
    IUnitOfWork unitOfWork) : IRequestHandler<AdditionImgUrlQueryRequest, PageResult<AdditionImgUrlDTO>>
{
    public async Task<PageResult<AdditionImgUrlDTO>> Handle(AdditionImgUrlQueryRequest request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting all restaurants");
        var (additionImgUrls, totalCount) = await unitOfWork.AdditionImgUrlReponsitory
            .GetAllMatchingAsync(request.SearchPhrase,
                request.PageSize,
                request.PageNumber,
                request.SortBy,
                request.SortDirection);

        var additionImgUrlDTO = mapper.Map<List<AdditionImgUrlDTO>>(additionImgUrls);

        var result = new PageResult<AdditionImgUrlDTO>(additionImgUrlDTO, totalCount, request.PageSize, request.PageNumber);
        return result;
    }
}
