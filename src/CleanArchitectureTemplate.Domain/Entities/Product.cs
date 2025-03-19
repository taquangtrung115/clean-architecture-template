using CleanArchitectureTemplate.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureTemplate.Domain.Entities
{
    public class Product : AuditableEntity<Guid>
    {
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public string Gender { get; set; }
        public Guid CategoryID { get; set; }
        public string StrSize { get; set; }
        public string ImageUrl { get; set; }
        public double TotalRating { get; set; }
        public double TotalReview { get; set; }
        public double Price { get; set; }
        public int PercentDisCount { get; set; }
        public bool IsInStock { get; set; } = true;
        public Guid BrandID { get; set; }
    }
}
