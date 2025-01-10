using Flunt.Notifications;
using LivrosAPI.Application.Contracts.Infrastructure.Services;
using LivrosAPI.Application.Contracts.Persistence.Repositories;
using LivrosAPI.Application.Responses;
using LivrosAPI.Domain.Models.Dtos;
using MediatR;

namespace LivrosAPI.Application.Features.Livro.Queries.RelatorioLivro
{
    public class RelatorioLivroHandler : IRequestHandler<RelatorioLivroQuery, RetornoService>
    {
        private readonly RetornoService _response;
        private readonly ILivroRepository _livroRepository;
        private readonly IRelatorioService _relatorioService;

        public RelatorioLivroHandler(ILivroRepository myRepository, IRelatorioService relatorioService)
        {
            _response = new RetornoService();
            _livroRepository = myRepository;
            _relatorioService = relatorioService;
        }

        public async Task<RetornoService> Handle(RelatorioLivroQuery request, CancellationToken cancellationToken)
        {
            try
            {                     
                List<LivroDto> list = await _livroRepository.ListarLivros();

                byte[] pdf = _relatorioService.GerarRelatorioLivrosPdf(new(list));

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
