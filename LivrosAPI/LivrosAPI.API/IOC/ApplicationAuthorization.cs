using LivrosAPI.Application.Models.Identity.Commands;
using LivrosAPI.Application.Models.Jwt;
using LivrosAPI.Infrastructure.Configurations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace LivrosAPI.API.IOC
{
    public static class ApplicationAuthorization
    {
        public static void AddAuthorizedMvc(this IServiceCollection services, IConfiguration configuration)
        {
            // Configurações de JWT sendo injetadas através do IOptions
            services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));

            AddJwtAuthorization(services);
            AddHttpContextAndAuthenticationServices(services);
        }

        private static void AddJwtAuthorization(IServiceCollection services)
        {
            // Registrando o serviço de configuração JwtSettings via IOptions
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    var serviceProvider = services.BuildServiceProvider();
                    var jwtSettings = serviceProvider.GetRequiredService<IOptions<JwtSettings>>().Value;

                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateActor = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtSettings.Issuer,
                        ValidAudience = jwtSettings.Audience,
                        IssuerSigningKey = SigningConfigurations.Instance.Key,

                        // Tolerância de tempo para diferenças entre servidores de até 5 minutos
                        ClockSkew = TimeSpan.FromMinutes(5)
                    };
                });

            // Configurando políticas de autorização
            services.AddAuthorization(options =>
            {
                options.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
              .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
              .RequireAuthenticatedUser()
              .Build());
            });
        }

        private static void AddHttpContextAndAuthenticationServices(IServiceCollection services)
        {
            // Registrando serviços relacionados ao contexto HTTP e autenticação
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<LoginCommand>(); // Exemplo de comando específico para login
        }
    }
}