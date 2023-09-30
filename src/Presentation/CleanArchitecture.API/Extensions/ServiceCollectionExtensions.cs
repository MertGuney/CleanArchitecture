using CleanArchitecture.Domain.Options;

namespace CleanArchitecture.API.Extensions;

public static class ServiceCollectionExtensions
{
    public static void ConfigureApiVersioning(this IServiceCollection services)
    {
        services.AddApiVersioning(options =>
        {
            options.DefaultApiVersion = new ApiVersion(1, 0);
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.ReportApiVersions = true;
            options.ApiVersionReader = ApiVersionReader.Combine(new UrlSegmentApiVersionReader());
        });
    }

    public static void ConfigureOptions(this IServiceCollection services,IConfiguration configuration)
    {
        services.Configure<AuthOption>(configuration.GetSection("Authentication"));
    }
}
