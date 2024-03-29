﻿namespace CleanArchitecture.Persistence.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddPersistenceLayer(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext(configuration);
        services.AddIdentity();
        services.AddServices();
        services.AddRepositories();
        services.AddEventDispatcher();
    }

    private static void AddDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
    }

    private static void AddEventDispatcher(this IServiceCollection services)
    {
        services.AddScoped<IDomainEventDispatcher, DomainEventDispatcher>();
    }

    private static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork))
                .AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
    }

    private static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<ICodeService, CodeService>();
    }

    private static void AddIdentity(this IServiceCollection services)
    {
        services.AddIdentity<User, Role>(opts =>
        {
            opts.Password.RequiredLength = 8;
            opts.Password.RequireDigit = false;

            opts.Lockout.AllowedForNewUsers = true;
            opts.Lockout.MaxFailedAccessAttempts = 3;
            opts.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
        }).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();
    }
}
