namespace CleanArchitecture.Persistence.Configurations;

public class ScopeConfiguration : IEntityTypeConfiguration<Scope>
{
    public void Configure(EntityTypeBuilder<Scope> builder)
    {
        builder.Property(x => x.Name).HasMaxLength(64);
        builder.Property(x => x.DisplayName).HasMaxLength(128);
    }
}