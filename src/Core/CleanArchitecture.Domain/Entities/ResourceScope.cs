namespace CleanArchitecture.Domain.Entities;

public class ResourceScope
{
    public Guid ResourceId { get; set; }
    public Resource Resource { get; set; }

    public Guid ScopeId { get; set; }
    public Scope Scope { get; set; }
}