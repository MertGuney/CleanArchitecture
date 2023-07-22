namespace CleanArchitecture.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddInfrastructureLayer(this IServiceCollection services)
    {
        services.AddServices();
        services.AddHttpContextAccessor();
        services.AddHttpClient();
    }

    private static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<IMailService, MailService>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<ICurrentUserService, CurrentUserService>();
    }

    private static void AddHttpClient(this IServiceCollection services)
    {
        services.AddHttpClient<IFacebookService, FacebookService>(opts =>
        {
            opts.BaseAddress = new Uri("https://graph.facebook.com");
        });
    }
}
