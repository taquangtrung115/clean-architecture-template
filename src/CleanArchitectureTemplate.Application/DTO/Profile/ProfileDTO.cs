using CleanArchitectureTemplate.Application.DTO.BaseDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureTemplate.Application.DTO.Profile
{
    public class ProfileDTO : AuditableEntity<Guid>
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public DateOnly? DayOfBirth { get; set; }
    }
}
