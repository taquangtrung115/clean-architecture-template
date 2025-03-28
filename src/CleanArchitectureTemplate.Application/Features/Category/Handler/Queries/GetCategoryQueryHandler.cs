using CleanArchitectureTemplate.Application.Common;
using CleanArchitectureTemplate.Application.DTO.Category;
using CleanArchitectureTemplate.Application.Features.Category.Request.Queries;
using CleanArchitectureTemplate.Application.Features.Product.Request.Queries;
using CleanArchitectureTemplate.Domain.Interfaces;
using MapsterMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureTemplate.Application.Features.Category.Handler.Queries;

public class GetCategoryQueryHandler(ILogger<GetProductAllQueryRequest> logger,
    IMapper mapper,
    IUnitOfWork unitOfWork) : IRequestHandler<GetCategoryQueryRequest, List<CategoryDTO>>
{
    public async Task<List<CategoryDTO>> Handle(GetCategoryQueryRequest request, CancellationToken cancellationToken)
    {
        var lstCategory = await unitOfWork.CategoryReponsitory.GetAllAsync();
        var lstCategoryDTO = mapper.Map<List<CategoryDTO>>(lstCategory);

        return lstCategoryDTO;
    }
}
