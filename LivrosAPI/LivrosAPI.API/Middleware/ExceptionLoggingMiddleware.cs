using LivrosAPI.Infrastructure.Services.Interfaces;
using LivrosAPI.Infrastructure.Services;
using LivrosAPI.Domain.Enums;
using LivrosAPI.Application.Responses;
using Microsoft.Extensions.DependencyInjection;

namespace LivrosAPI.API.Middleware
{
    public class ExceptionLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public ExceptionLoggingMiddleware(RequestDelegate next, IServiceScopeFactory serviceScopeFactory)
        {
            _next = next;
            _serviceScopeFactory = serviceScopeFactory;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {               
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var _loggingService = scope.ServiceProvider.GetRequiredService<ILoggingService>();


            _loggingService.LogError(new LogModel
            {
                Chave = EChaveLog.EXCEPTION_NAO_TRATADA,
                Dados = new
                {
                    Path = context.Request.Path,
                    QueryString = context.Request.QueryString.ToString(),
                    Method = context.Request.Method,
                    ExceptionMessage = exception.Message,
                    ExceptionStackTrace = exception.StackTrace
                }
            }, exception);
            
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = "application/json";

            ServiceResponse retorno = new()
            {
                Status = ServiceResponseStatus.Error,
                Message = "Um erro inesperado ocorreu, entre em contato com o suporte.",
                StatusCode = System.Net.HttpStatusCode.InternalServerError

            };
            
            await context.Response.WriteAsJsonAsync(retorno);
        }
    }
}
