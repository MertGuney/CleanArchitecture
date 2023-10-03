namespace CleanArchitecture.Domain.Entities;

public class Client : BaseAuditableEntity
{
    public string ClientId { get; set; }
    public string ClientName { get; set; }
    public string ClientSecret { get; set; }
    public int AccessTokenLifetime { get; set; }
    public int RefreshTokenLifetime { get; set; }

    public virtual ICollection<ClientScope> ClientScopes { get; set; }
}