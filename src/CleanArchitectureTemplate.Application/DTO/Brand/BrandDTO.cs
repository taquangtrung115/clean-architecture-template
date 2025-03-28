using CleanArchitectureTemplate.Application.DTO.BaseDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureTemplate.Application.DTO.Brand;

public class BrandDTO : AuditableEntity
{
    public string BrandCode { get; set; }
    public string BrandName { get; set; }
}
