namespace CleanArchitecture.Persistence.Configurations;

public class AspNetCoreUserCodeConfiguration : IEntityTypeConfiguration<AspNetCoreUserCode>
{
    public void Configure(EntityTypeBuilder<AspNetCoreUserCode> builder)
    {
        builder.Property(x => x.Value).HasMaxLength(6).IsRequired();
    }
}