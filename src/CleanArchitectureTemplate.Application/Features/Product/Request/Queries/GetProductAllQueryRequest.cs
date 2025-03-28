using CleanArchitectureTemplate.Application.Common;
using CleanArchitectureTemplate.Application.DTO.Product;
using CleanArchitectureTemplate.Domain.Constants;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureTemplate.Application.Features.Product.Request.Queries;

public class GetProductAllQueryRequest : IRequest<PageResult<ProductGetAllDTO>>
{
    public string? SearchPhrase { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public string? SortBy { get; set; }
    public string? Brand { get; set; }
    public string? Category { get; set; }
    public string? Gender { get; set; }
    public string? Order { get; set; }
    public string? Price { get; set; }
    public string? ProductName { get; set; }
    public bool? in_stock { get; set; }
    public DateTime? Date { get; set; }
    public SortDirection SortDirection { get; set; }
}
