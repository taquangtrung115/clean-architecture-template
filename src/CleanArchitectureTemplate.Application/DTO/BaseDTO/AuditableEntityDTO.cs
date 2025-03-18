using CleanArchitectureTemplate.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureTemplate.Application.DTO.BaseDTO;
public abstract class AuditableEntity<TKey> : Entity<TKey>
{
    public DateTime CreationDate { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? ModificationDate { get; set; }
    public string? ModificationBy { get; set; }
    public bool IsDeleted { get; set; }
}