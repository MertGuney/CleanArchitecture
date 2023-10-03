namespace CleanArchitecture.Persistence.Configurations;

public class ClientConfiguration : IEntityTypeConfiguration<Client>
{
    public void Configure(EntityTypeBuilder<Client> builder)
    {
        builder.Property(x => x.ClientId).HasMaxLength(64);
        builder.Property(x => x.ClientName).HasMaxLength(128);
        builder.Property(x => x.ClientSecret).HasMaxLength(128);

        builder.Property(x => x.Type)
            .HasConversion(c => c.ToString(),
                p => (ClientTypesEnum)Enum.Parse(typeof(ClientTypesEnum), p))
            .HasMaxLength(16);
    }
}