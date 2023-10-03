namespace CleanArchitecture.Persistence.Configurations;

public class TokenConfiguration : IEntityTypeConfiguration<Token>
{
    public void Configure(EntityTypeBuilder<Token> builder)
    {
        builder.Property(x => x.Key).HasMaxLength(128);
        builder.Property(x => x.ClientId).HasMaxLength(64);
        builder.Property(x => x.Algorithm).HasMaxLength(32);
        builder.Property(x => x.SubjectId).HasMaxLength(64);

        builder.Property(x => x.Type)
            .HasConversion(c => c.ToString(),
                p => (TokenTypesEnum)Enum.Parse(typeof(TokenTypesEnum), p))
            .HasMaxLength(16);
    }
}