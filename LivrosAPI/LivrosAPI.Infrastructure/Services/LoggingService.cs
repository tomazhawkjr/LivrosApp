using LivrosAPI.Domain.Enums;
using LivrosAPI.Infrastructure.Services.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Serilog;

namespace LivrosAPI.Infrastructure.Services
{
    public class LogModel
    {
        public required EChaveLog Chave { get; set; } 
        public dynamic? Dados { get; set; } 

        public static LogModel Create(EChaveLog chave, dynamic? dados)
        {
            return new() { Chave = chave, Dados = dados };
        }
    }

    public class LoggingService : ILoggingService
    {
        private readonly ILogger _logger;
        private readonly JsonSerializerSettings jsonSettings = new JsonSerializerSettings
        {
            Converters = { new StringEnumConverter() },
            Formatting = Formatting.Indented
        };

        public LoggingService(ILogger logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Loga informações usando Serilog.
        /// </summary>
        /// <param name="logModel">Dados para log.</param>
        public void LogInformation(LogModel logModel)
        {
            _logger.Information("{@log}",
                JsonConvert.SerializeObject(logModel, jsonSettings));
        }

        /// <summary>
        /// Loga erros usando Serilog.
        /// </summary>
        /// <param name="logModel">Dados para log.</param>
        /// <param name="exception">Exceção opcional associada.</param>
        public void LogError(LogModel logModel, Exception? exception = null)
        {
            if (exception != null)
            {
                _logger.Error(exception, "{@log}",
                    JsonConvert.SerializeObject(logModel, jsonSettings));
            }
            else
            {
                _logger.Error("{@log}",
                    JsonConvert.SerializeObject(logModel, jsonSettings));
            }
        }
    }

}
