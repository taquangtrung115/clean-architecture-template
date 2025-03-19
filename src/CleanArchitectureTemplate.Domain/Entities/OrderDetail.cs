using CleanArchitectureTemplate.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureTemplate.Domain.Entities
{
    public class OrderDetail : AuditableEntity<Guid>
    {
        public Guid OrderID { get; set; }
        public Guid ProductID { get; set; }
        public int Amout { get; set; }
        public string SizeSelected { get; set; }
        /// <summary>
        /// wish list
        /// </summary>
        public bool IsInWishList { get; set; }
    }
}
