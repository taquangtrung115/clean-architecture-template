using CleanArchitectureTemplate.Application.DTO.BaseDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureTemplate.Application.DTO.AdditionImgUrl;

public class AdditionImgUrlDTO : AuditableEntity
{
    public Guid ProductID { get; set; }
    public string ImageUrl { get; set; }
    public bool IsLocalFolder { get; set; }
}
