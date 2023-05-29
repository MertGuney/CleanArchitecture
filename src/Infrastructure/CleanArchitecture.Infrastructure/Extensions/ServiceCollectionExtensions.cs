using CleanArchitecture.Application.Interfaces.Services;
using CleanArchitecture.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddInfrastructureLayer(this IServiceCollection services)
        {
            services.AddServices();
            services.AddHttpContextAccessor();
        }

        private static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IMailService, MailService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<ICurrentUserService, CurrentUserService>();
        }
    }
}
