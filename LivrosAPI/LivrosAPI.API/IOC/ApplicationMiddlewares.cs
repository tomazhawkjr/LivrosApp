using LivrosAPI.API.Middleware;
using LivrosAPI.Application.Models.Jwt;

namespace LivrosAPI.API.IOC
{
    public static class ApplicationMiddlewares
    {
        public static void AddMiddlewares(this WebApplication application)
        {
            application.UseMiddleware<ExceptionLoggingMiddleware>();
            application.UseMiddleware<TimeExecutionLoggingMiddleware>();
        }
    }
}
