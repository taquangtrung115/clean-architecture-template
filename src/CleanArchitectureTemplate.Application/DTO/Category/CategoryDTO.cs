using CleanArchitectureTemplate.Application.DTO.BaseDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureTemplate.Application.DTO.Category;

public class CategoryDTO : AuditableEntity
{
    public string CategoryCode { get; set; }
    public string CategoryName { get; set; }
}
