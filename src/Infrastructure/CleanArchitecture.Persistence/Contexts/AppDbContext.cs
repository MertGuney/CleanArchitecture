namespace CleanArchitecture.Persistence.Contexts;

public class AppDbContext : IdentityDbContext<User, Role, Guid>
{
    private readonly IDomainEventDispatcher _dispatcher;

    public AppDbContext(DbContextOptions<AppDbContext> options, IDomainEventDispatcher dispatcher) : base(options)
    {
        _dispatcher = dispatcher;
    }

    public DbSet<Token> Tokens { get; set; }
    public DbSet<Scope> Scopes { get; set; }
    public DbSet<Client> Clients { get; set; }
    public DbSet<Resource> Resources { get; set; }
    public DbSet<AspNetCoreUserCode> AspNetCoreUserCodes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<ClientScope>()
            .HasKey(sc => new { sc.ScopeId, sc.ClientId });

        modelBuilder
            .Entity<ResourceScope>()
            .HasKey(sc => new { sc.ScopeId, sc.ResourceId });

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
    {
        int result = await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        if (_dispatcher == null) return result;

        var entitiesWithEvents = ChangeTracker.Entries<BaseEntity>().Select(x => x.Entity).Where(x => x.DomainEvents.Any()).ToArray();

        await _dispatcher.DispatchAndClearEvents(entitiesWithEvents);

        return result;
    }

    public override int SaveChanges()
    {
        return SaveChangesAsync().GetAwaiter().GetResult();
    }
}