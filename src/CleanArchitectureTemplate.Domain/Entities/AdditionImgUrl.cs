using CleanArchitectureTemplate.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureTemplate.Domain.Entities
{
    public class AdditionImgUrl : AuditableEntity
    {
        public Guid ProductID { get; set; }
        public string ImageUrl { get; set; }
        public bool IsLocalFolder { get; set; } = true;
    }
}
