using CleanArchitectureTemplate.Application.Common;
using CleanArchitectureTemplate.Application.DTO.AdditionImgUrl;
using CleanArchitectureTemplate.Domain.Constants;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureTemplate.Application.Features.AdditionImgUrl.Request.Queries.GetAddtionImgUrl;

public class AdditionImgUrlQueryRequest : IRequest<PageResult<AdditionImgUrlDTO>>
{
    public string? SearchPhrase { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public string? SortBy { get; set; }
    public SortDirection SortDirection { get; set; }
}
