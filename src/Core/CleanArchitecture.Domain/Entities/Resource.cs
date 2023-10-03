namespace CleanArchitecture.Domain.Entities;

public class Resource : BaseAuditableEntity
{
    public string Name { get; set; }
    public string DisplayName { get; set; }

    public virtual ICollection<ResourceScope> ResourceScopes { get; set; }
}