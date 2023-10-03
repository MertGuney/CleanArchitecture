namespace CleanArchitecture.Persistence.Configurations;

public class ResourceConfiguration : IEntityTypeConfiguration<Resource>
{
    public void Configure(EntityTypeBuilder<Resource> builder)
    {
        builder.Property(x => x.Name).HasMaxLength(64);
        builder.Property(x => x.DisplayName).HasMaxLength(128);
    }
}