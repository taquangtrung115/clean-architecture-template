using CleanArchitectureTemplate.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureTemplate.Domain.Entities
{
    public class Review : AuditableEntity
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
}
