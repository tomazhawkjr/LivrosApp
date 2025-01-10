using Flunt.Notifications;
using LivrosAPI.Application.Contracts;
using LivrosAPI.Application.Contracts.Infrastructure.Interfaces;
using LivrosAPI.Application.Contracts.Persistence.Repositories;
using LivrosAPI.Application.Responses;
using MediatR;

namespace LivrosAPI.Application.Features.Livro.Commands.DeletarLivro
{
    public class DeletarLivroHandler : IRequestHandlerBase<DeletarLivroCommand>
    {
        private readonly RetornoService _response;
        private readonly ILivroRepository _livroRepository;

        public DeletarLivroHandler(ILivroRepository livroRepository)
        {
            _response = new RetornoService();
            _livroRepository = livroRepository;
        }

        public async Task<RetornoService> Handle(DeletarLivroCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _livroRepository.DeleteLivro(request.Id);

                _response.Sucesso = true;
                return _response;
            }
            catch (Exception ex)
            {
                Notification notification = new Notification("Deletar", ex.Message);
                _response.Sucesso = false;
                _response.AddNotification(notification);
                return _response;
            }
        }
    }
}
