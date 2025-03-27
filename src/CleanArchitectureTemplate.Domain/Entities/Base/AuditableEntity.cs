namespace CleanArchitectureTemplate.Domain.Entities.Base;

public class AuditableEntity : Entity
{
    public DateTime CreationDate { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? ModificationDate { get; set; }
    public string? ModificationBy { get; set; }
    public bool IsDeleted { get; set; }
}