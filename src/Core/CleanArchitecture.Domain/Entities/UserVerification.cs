using CleanArchitecture.Domain.Common;

namespace CleanArchitecture.Domain.Entities
{
    public class UserVerification : BaseAuditableEntity
    {
        public string Code { get; set; }
        public string UserId { get; set; }
        public DateTime ExpireDate { get; set; }

        public User User { get; set; }
    }
}
