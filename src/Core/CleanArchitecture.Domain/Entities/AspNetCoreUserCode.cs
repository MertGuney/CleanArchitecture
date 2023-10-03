namespace CleanArchitecture.Domain.Entities;

public class AspNetCoreUserCode : BaseAuditableEntity
{
    public Guid UserId { get; set; }
    public string Value { get; set; }
    public bool IsVerified { get; set; }
    public DateTime ExpireDate { get; set; }

    public User User { get; set; }

    public AspNetCoreUserCode()
    { }

    public AspNetCoreUserCode(string value, Guid userId, bool isVerified = false, int seconds = 200)
    {
        Value = value;
        UserId = userId;
        IsVerified = isVerified;
        ExpireDate = DateTime.UtcNow.AddSeconds(seconds);
    }
}