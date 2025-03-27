using CleanArchitectureTemplate.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureTemplate.Domain.Entities
{
    public class Brand : AuditableEntity
    {
        public string BrandCode { get; set; }
        public string BrandName { get; set; }
    }
}
