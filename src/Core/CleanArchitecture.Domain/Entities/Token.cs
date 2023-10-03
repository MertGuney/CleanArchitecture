namespace CleanArchitecture.Domain.Entities;

public class Token : BaseAuditableEntity
{
    public string Key { get; set; }
    public string ClientId { get; set; }
    public string Algorithm { get; set; }
    public string SubjectId { get; set; }
    public DateTime Expiration { get; set; }
    public TokenTypesEnum Type { get; set; }
}