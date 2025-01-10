using LivrosAPI.Application.Contracts.Infrastructure.Services;
using LivrosAPI.Application.Contracts.Jwt;
using LivrosAPI.Infrastructure.Services;
using LivrosAPI.Infrastructure.Services.Interfaces;
using LivrosAPI.Infrastructure.Services.Jwt;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LivrosAPI.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ILoggingService, LoggingService>();
            services.AddScoped<IConfigurationService, ConfigurationService>();
            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<IRelatorioService, RelatorioService>();

            return services;
        }
    }
}
