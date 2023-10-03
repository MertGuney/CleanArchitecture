namespace CleanArchitecture.Domain.Common;

public abstract class BaseAuditableEntity : BaseEntity, IAuditableEntity
{
    [StringLength(64)]
    public string CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    [StringLength(64)]
    public string UpdatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public void AddCreator(string createdBy)
    {
        CreatedBy = createdBy;
        CreatedDate = DateTime.UtcNow;
    }

    public void AddLastModifier(string updatedBy)
    {
        UpdatedBy = updatedBy;
        UpdatedDate = DateTime.UtcNow;
    }
}