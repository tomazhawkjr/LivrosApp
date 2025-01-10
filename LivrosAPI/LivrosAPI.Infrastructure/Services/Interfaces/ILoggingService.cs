using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LivrosAPI.Infrastructure.Services.Interfaces
{
    public interface ILoggingService
    {
        /// <summary>
        /// Loga informações.
        /// </summary>
        /// <param name="logModel">Dados para log.</param>
        void LogInformation(LogModel logModel);

        /// <summary>
        /// Loga erros.
        /// </summary>
        /// <param name="logModel">Dados para log.</param>
        /// <param name="exception">Exceção opcional associada.</param>
        void LogError(LogModel logModel, Exception? exception = null);
    }

}
