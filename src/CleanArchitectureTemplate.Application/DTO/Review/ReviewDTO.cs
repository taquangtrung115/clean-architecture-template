using CleanArchitectureTemplate.Application.DTO.BaseDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureTemplate.Application.DTO.Review;

public class ReviewDTO : AuditableEntity
{
    public string ReviewCode { get; set; }
    public string UserName { get; set; }
    public string UserImage { get; set; }
    public string Location { get; set; }
    public string ReviewTitle { get; set; }
    public string ReviewContent { get; set; }
    public double Rating { get; set; }
    public DateTime Date { get; set; } = DateTime.Now;
    public Guid ProductID { get; set; }
}
