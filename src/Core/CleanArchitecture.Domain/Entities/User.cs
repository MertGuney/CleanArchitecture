namespace CleanArchitecture.Domain.Entities;

public class User : IdentityUser<Guid>
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public DateTime? Birthdate { get; set; }

    public string CreatedBy { get; set; }
    public DateTime CreatedDate { get; set; }
    public string UpdatedBy { get; set; }
    public DateTime? UpdatedDate { get; set; }
}
