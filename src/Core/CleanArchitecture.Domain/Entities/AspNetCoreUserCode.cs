namespace CleanArchitecture.Domain.Entities;

public class AspNetCoreUserCode : BaseAuditableEntity
{
    public string Value { get; set; }
    public Guid UserId { get; set; }
    public bool IsVerified { get; set; }
    public DateTime ExpireDate { get; set; }

    public User User { get; set; }

    public AspNetCoreUserCode() { }

    public AspNetCoreUserCode(string value, Guid userId, int seconds = 200)
    {
        Value = value;
        UserId = userId;
        IsVerified = false;
        ExpireDate = DateTime.UtcNow.AddSeconds(seconds);
    }
}
