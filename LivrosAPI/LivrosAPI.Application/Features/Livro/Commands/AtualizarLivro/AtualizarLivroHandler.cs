using Flunt.Notifications;
using LivrosAPI.Application.Contracts;
using LivrosAPI.Application.Contracts.Infrastructure.Interfaces;
using LivrosAPI.Application.Contracts.Persistence.Repositories;
using LivrosAPI.Application.Features.Livro.Commands.AtualizarLivro;
using LivrosAPI.Application.Responses;
using MediatR;

namespace LivrosAPI.Application.Features.Livro.Commands.AtualizarLivro
{
    public class AtualizarLivroHandler : IRequestHandlerBase<AtualizarLivroCommand>
    {
        private readonly RetornoService _response;
        private readonly ILivroRepository _livroRepository;

        public AtualizarLivroHandler(ILivroRepository livroRepository)
        {
            _response = new RetornoService();
            _livroRepository = livroRepository;
        }

        public async Task<RetornoService> Handle(AtualizarLivroCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _livroRepository.UpdateLivro(request);
                _response.Sucesso = true;
                return _response;
            }
            catch (Exception ex)
            {               
                _response.Sucesso = false;
                _response.AddNotification(new("Atualizar Livro", ex.Message));
                return _response;
            }
        }
    }
}
