using LivrosAPI.Application.Contracts.Infrastructure.Services;
using LivrosAPI.Application.Contracts.Jwt;
using LivrosAPI.Application.Models.Jwt;
using LivrosAPI.Identity.Repositories.Interfaces;
using LivrosAPI.Identity.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LivrosAPI.Identity.IOC
{
    public static class InfraestrutureIdentityRegistration
    {
        public static IServiceCollection AddInfrastructureIdentity(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IApplicationUserRepository, ApplicationUserRepository>();
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
            services.AddScoped<IClientRepository, ClientRepository>();
            services.AddSingleton<JwtSettings>();
            
            return services;
        }
    }
}
