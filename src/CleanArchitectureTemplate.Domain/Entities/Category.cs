using CleanArchitectureTemplate.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureTemplate.Domain.Entities
{
    public class Category : AuditableEntity<Guid>
    {
        public string CategoryCode { get; set; }
        public string CategoryName { get; set; }
    }
}
