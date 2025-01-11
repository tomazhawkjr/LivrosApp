using Flunt.Notifications;
using LivrosAPI.Application.Contracts.Infrastructure.Services;
using LivrosAPI.Application.Contracts.Persistence.Repositories;
using LivrosAPI.Application.Responses;
using LivrosAPI.Domain.Models.Dtos;
using MediatR;

namespace LivrosAPI.Application.Features.Livro.Queries.RelatorioLivoByAutor
{
    public class RelatorioLivroByAutorHandler : IRequestHandler<RelatorioLivroByAutorQuery, RetornoService>
    {
        private readonly RetornoService _response;
        private readonly ILivroRepository _livroRepository;
        private readonly IRelatorioService _relatorioService;

        public RelatorioLivroByAutorHandler(ILivroRepository myRepository, IRelatorioService relatorioService)
        {
            _response = new RetornoService();
            _livroRepository = myRepository;
            _relatorioService = relatorioService;
        }

        public async Task<RetornoService> Handle(RelatorioLivroByAutorQuery request, CancellationToken cancellationToken)
        {
            try
            {
                List<LivroByAutorDto> list = await _livroRepository.ListarLivrosByAutor();

                byte[] pdf = _relatorioService.GerarRelatorioLivrosByAutorPdf(new(list));

                _response.Sucesso = true;
                _response.AddValue(pdf);

                return _response;
            }
            catch (Exception ex)
            {
                _response.Sucesso = false;
                _response.AddNotification(new("Erro ao buscar Livros", ex.Message));
                return _response;
            }
        }
    }
}
