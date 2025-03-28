using CleanArchitectureTemplate.Application.Common;
using CleanArchitectureTemplate.Application.DTO.Category;
using CleanArchitectureTemplate.Application.DTO.Product;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureTemplate.Application.Features.Category.Request.Queries;

public class GetCategoryQueryRequest : IRequest<List<CategoryDTO>>
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 50;
    public string? SearchText { get; set; }
}
