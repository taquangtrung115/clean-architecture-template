using CleanArchitectureTemplate.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureTemplate.Domain.Entities
{
    public class Order : AuditableEntity
    {
        public int UserID { get; set; }
        public string OrderStatus { get; set; }
        public int TotalDetail { get; set; }
    }
}
