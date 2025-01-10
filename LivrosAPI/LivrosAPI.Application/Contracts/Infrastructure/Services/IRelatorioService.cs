using LivrosAPI.Domain.Reports;

namespace LivrosAPI.Application.Contracts.Infrastructure.Services
{
    public interface IRelatorioService
    {
        public byte[] GerarRelatorioLivrosPdf(LivrosReport livroReport);
    }
}
