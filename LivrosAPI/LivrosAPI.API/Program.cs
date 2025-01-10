using CoreZipCode.Interfaces;
using LivrosAPI.API.IOC;
using LivrosAPI.API.Services;
using LivrosAPI.Application;
using LivrosAPI.Application.Contracts;
using LivrosAPI.Application.Contracts.Infrastructure.Services;
using LivrosAPI.Application.Contracts.Jwt;
using LivrosAPI.Application.Models;
using LivrosAPI.Application.Models.Identity;
using LivrosAPI.Application.Models.Jwt;
using LivrosAPI.Identity;
using LivrosAPI.Identity.Repositories;
using LivrosAPI.Identity.Repositories.Interfaces;       
using LivrosAPI.Infrastructure;
using LivrosAPI.Infrastructure.Services;
using LivrosAPI.Infrastructure.Services.Jwt;
using LivrosAPI.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Serilog;
using Serilog.Events;
using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

var env = builder.Environment;
builder.Configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                     .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
                     .AddEnvironmentVariables();

//var logFilePath = "logs/execution_log.txt";

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
    .MinimumLevel.Override("System", LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateLogger();

builder.Host.UseSerilog(Log.Logger);
//builder.Services.AddSingleton(Log.Logger);

builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver(); // Nomeação camelCase
    options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore; // Ignorar valores nulos
    options.SerializerSettings.Converters.Add(new StringEnumConverter()); // Converte enums para strings
});

// Adiciona servi�os ao cont�iner.
builder.Services.AddApplicationServices();
builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddInfrastructureServices(builder.Configuration);

builder.Services.AddMemoryCache();

builder.Services.AddScoped<ILoggedInUserService, LoggedInUserService>();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
{    
    options.Stores.MaxLengthForKeys = 128;
    options.Password.RequiredLength = 6;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireDigit = false;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

builder.Services.AddSingleton<IConfiguration>(builder.Configuration);
builder.Services.AddLogging();
builder.Services.AddHealthChecks();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1",
        new OpenApiInfo
        {
            Title = "LivroAPI API",
            Version = "v1",
            Description = "API de Negócio do Aplicativo LivroAPI, autenticação via oauth.",
        });

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Cabeçalho de autorização JWT usando o esquema Bearer (Exemplo: 'Bearer 12345abcdef')",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    //options.AddSecurityRequirement(new OpenApiSecurityRequirement
    //{
    //    {
    //        new OpenApiSecurityScheme
    //        {
    //            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
    //        },
    //        Array.Empty<string>()
    //    }
    //});
});

builder.Services.Configure<MySettings>(builder.Configuration.GetSection("MySettings"));

builder.Services.AddScoped<IApplicationUserRepository, ApplicationUserRepository>();
builder.Services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
builder.Services.AddScoped<IClientRepository, ClientRepository>();
builder.Services.AddScoped<IConfigurationService, ConfigurationService>();

builder.Services.AddSingleton<JwtSettings>();
builder.Services.AddScoped<IJwtService, JwtService>();

builder.Services.AddSingleton<ZipCodeBaseService, CoreZipCode.Services.ZipCode.ViaCepApi.ViaCep>();

string portalUrl = builder.Configuration["MySettings:PortalUrl"];

// Configura��o do CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins", policy =>
    {
        policy.WithOrigins(portalUrl, $"{portalUrl}\\")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });

    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

builder.Services.AddRazorPages();

builder.Services.AddAuthorizedMvc(builder.Configuration);

var app = builder.Build();

app.AddMiddlewares();

// Configura o pipeline de requisi��es HTTP.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();

    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.RoutePrefix = "swagger";
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "LivroAPI");
    });
}
else
{
    app.UseDeveloperExceptionPage();

    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.RoutePrefix = "swagger";
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "LivroAPI");
    });

    var option = new RewriteOptions();
    option.AddRedirect("^$", "swagger");
    app.UseRewriter(option);
}

app.UseHttpsRedirection();

app.UseRouting();

//#if DEBUG
app.UseCors("AllowAll");
//#else
//    app.UseCors("AllowSpecificOrigins");
//#endif

app.UseAuthentication();
app.UseAuthorization();

app.MapHealthChecks("/healthcheck");
app.MapControllers();
app.MapRazorPages();

app.Run();
