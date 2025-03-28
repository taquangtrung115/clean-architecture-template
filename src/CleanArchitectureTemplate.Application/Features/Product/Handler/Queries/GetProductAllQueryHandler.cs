using CleanArchitectureTemplate.Application.Common;
using CleanArchitectureTemplate.Application.DTO.Brand;
using CleanArchitectureTemplate.Application.DTO.Category;
using CleanArchitectureTemplate.Application.DTO.Product;
using CleanArchitectureTemplate.Application.DTO.Review;
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

namespace CleanArchitectureTemplate.Application.Features.Product.Handler.Queries;

public class GetProductAllQueryHandler(ILogger<GetProductAllQueryRequest> logger,
    IMapper mapper,
    IUnitOfWork unitOfWork) : IRequestHandler<GetProductAllQueryRequest, PageResult<ProductGetAllDTO>>
{
    public async Task<PageResult<ProductGetAllDTO>> Handle(GetProductAllQueryRequest request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting all products");
        var (products, totalCount) = await unitOfWork.ProductReponsitory
            .GetAllMatchingAsync(request.SearchPhrase,
                request.PageSize,
                request.PageNumber,
                request.SortBy,
                request.SortDirection);

        var lstReview = await unitOfWork.ReviewReponsitory.GetAllAsync();
        var lstReviewDTO = mapper.Map<List<ReviewDTO>>(lstReview);

        var lstBrand = await unitOfWork.BrandReponsitory.GetAllAsync();
        var lstBrandDTO = mapper.Map<List<BrandDTO>>(lstBrand);

        var lstCategory = await unitOfWork.CategoryReponsitory.GetAllAsync();
        var lstCategoryDTO = mapper.Map<List<CategoryDTO>>(lstCategory);

        var productsDTO = mapper.Map<List<ProductGetAllDTO>>(products);
        foreach (var item in productsDTO)
        {
            item.Reviews = new List<ReviewDTO>();
            item.Reviews = lstReviewDTO.Where(x => x.ProductID == item.Id).ToList();
            var category = lstCategoryDTO.FirstOrDefault(x => x.Id == item.CategoryID);
            if (category != null)
            {
                item.CategoryName = category.CategoryName;
                item.CategoryCode = category.CategoryCode;
            }
            var brand = lstBrandDTO.FirstOrDefault(x => x.Id == item.BrandID);
            if (brand != null)
            {
                item.BrandName = brand.BrandName;
            }
            item.AvailableSizes = new List<string>();
            item.AvailableSizes = item.StrSize.Split(",").ToList();
        }
        var result = new PageResult<ProductGetAllDTO>(productsDTO, totalCount, request.PageSize, request.PageNumber);
        return result;
    }
}
