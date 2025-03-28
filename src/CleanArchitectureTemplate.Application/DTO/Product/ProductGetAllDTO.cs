using CleanArchitectureTemplate.Application.DTO.BaseDTO;
using CleanArchitectureTemplate.Application.DTO.Review;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureTemplate.Application.DTO.Product;

public class ProductGetAllDTO : AuditableEntity
{
    public string StrSize { get; set; }
    public Guid BrandID { get; set; }
    public Guid CategoryID { get; set; }
    public string ProductName { get; set; }
    public string Description { get; set; }
    public string Gender { get; set; }
    public string CategoryCode { get; set; }
    public string CategoryName { get; set; }
    public string BrandName { get; set; }
    public string ProductCode { get; set; }
    public string ImageUrl { get; set; }
    public List<string> AvailableSizes { get; set; }
    public double Rating { get; set; } = 0;
    public double Price { get; set; } = 0;
    public double TotalReviewCount { get; set; } = 0;
    public List<ReviewDTO> Reviews { get; set; }
    public bool IsInStock { get; set; }
}
