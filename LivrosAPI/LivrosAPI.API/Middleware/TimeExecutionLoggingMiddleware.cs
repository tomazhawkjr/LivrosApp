using LivrosAPI.Infrastructure.Services;
using LivrosAPI.Infrastructure.Services.Interfaces;
using Serilog;
using System.Diagnostics;

namespace LivrosAPI.API.Middleware
{
    public class TimeExecutionLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public TimeExecutionLoggingMiddleware(RequestDelegate next, IServiceScopeFactory serviceScopeFactory)
        {
            _next = next;
            _serviceScopeFactory = serviceScopeFactory;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var request = context.Request;

            using var scope = _serviceScopeFactory.CreateScope();
            var _loggingService = scope.ServiceProvider.GetRequiredService<ILoggingService>();


            string requestBody = string.Empty;
            if (request.Method == HttpMethods.Post || request.Method == HttpMethods.Put)
            {
                request.EnableBuffering();
                using var reader = new StreamReader(request.Body, leaveOpen: true);
                requestBody = await reader.ReadToEndAsync();
                request.Body.Position = 0;
            }

            var stopwatch = Stopwatch.StartNew();

            try
            {                
                await _next(context);
            }
            finally
            {
                stopwatch.Stop();

                // Tempo de execução e status da resposta
                var elapsedMilliseconds = stopwatch.ElapsedMilliseconds;
                var response = context.Response;
                
                _loggingService.LogInformation(LogModel.Create(Domain.Enums.EChaveLog.TEMPO_EXECUCAO, new
                {
                    TempoExecucao = elapsedMilliseconds,                    
                    request.Method,
                    request.Path,
                    response.StatusCode,
                    Request = requestBody
                }));               
            }
        }
    }

}
