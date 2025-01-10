using Flunt.Notifications;
using LivrosAPI.Application.Contracts.Persistence.Repositories;
using LivrosAPI.Application.Responses;
using LivrosAPI.Domain.Models.Dtos;
using MediatR;

namespace LivrosAPI.Application.Features.Livro.Queries.BuscarLivro
{
    public class BuscarLivroHandler : IRequestHandler<BuscarLivroQuery, RetornoService>
    {
        private readonly RetornoService _response;
        private readonly ILivroRepository _livroRepository;

        public BuscarLivroHandler(ILivroRepository myRepository)
        {
            _response = new RetornoService();
            _livroRepository = myRepository;
        }

        public async Task<RetornoService> Handle(BuscarLivroQuery request, CancellationToken cancellationToken)
        {
            try
            {                     
                List<LivroDto> list = await _livroRepository.ListarLivros();

                _response.Sucesso = true;
                _response.AddValue(list);

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
