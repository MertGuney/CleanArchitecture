using CleanArchitecture.Domain.Common;

namespace CleanArchitecture.Domain.Entities
{
    public class VerificationCode : BaseAuditableEntity
    {
        public string Value { get; set; }
        public string UserId { get; set; }
        public bool IsVerified { get; set; }
        public DateTime ExpireDate { get; set; }

        public User User { get; set; }

        public VerificationCode() { }

        public VerificationCode(string value, string userId)
        {
            Value = value;
            UserId = userId;
            IsVerified = false;
            ExpireDate = DateTime.Now.AddSeconds(200);
        }
    }
}
