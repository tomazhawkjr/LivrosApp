using Flunt.Notifications;
using LivrosAPI.Application.Contracts;
using LivrosAPI.Application.Contracts.Persistence.Repositories;
using LivrosAPI.Application.Features.Livro.Commands.CadastrarLivro;
using LivrosAPI.Application.Responses;
using MediatR;

namespace LivrosAPI.Application.Features.Livro.Commands.CadastrarLivro
{
    public class CadastrarLivroHandler : IRequestHandler<CadastrarLivroCommand, RetornoService>
    {
        private readonly RetornoService _response;
        private readonly ILivroRepository _livroRepository;
        private readonly ILoggedInUserService _loggedInUserService;

        public CadastrarLivroHandler(ILivroRepository livroRepository, ILoggedInUserService loggedInUserService)
        {
            _response = new RetornoService();
            _livroRepository = livroRepository;
            _loggedInUserService = loggedInUserService;
        }

        public async Task<RetornoService> Handle(CadastrarLivroCommand request, CancellationToken cancellationToken)
        {
            try
            {
                request.Id = null;
                _response.AddValue(await _livroRepository.InsertAsync(request));

                _response.Sucesso = true;
                return _response;
            }
            catch (Exception ex)
            {
                _response.Sucesso = false;
                _response.AddNotification(new("Erro ao cadastrar Livro", ex.Message));
                return _response;
            }
        }
    }
}
