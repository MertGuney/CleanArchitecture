namespace CleanArchitecture.Domain.Entities;

public class Scope : BaseAuditableEntity
{
    public string Name { get; set; }
    public string DisplayName { get; set; }

    public virtual ICollection<ClientScope> ClientScopes { get; set; }
    public virtual ICollection<ResourceScope> ResourceScopes { get; set; }
}