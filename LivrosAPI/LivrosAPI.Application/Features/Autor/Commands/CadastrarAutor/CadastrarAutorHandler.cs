using Flunt.Notifications;
using LivrosAPI.Application.Contracts;
using LivrosAPI.Application.Contracts.Persistence.Repositories;
using LivrosAPI.Application.Features.Livro.Commands.CadastrarLivro;
using LivrosAPI.Application.Responses;
using MediatR;

namespace LivrosAPI.Application.Features.Autor.Commands.CadastrarAutor
{
    public class CadastrarAutorHandler : IRequestHandler<CadastrarAutorCommand, RetornoService>
    {
        private readonly RetornoService _response;
        private readonly IRepository<Domain.Entities.Autor> _autorRepository;
        private readonly ILoggedInUserService _loggedInUserService;

        public CadastrarAutorHandler(IRepository<Domain.Entities.Autor> livroRepository, ILoggedInUserService loggedInUserService)
        {
            _response = new RetornoService();
            _autorRepository = livroRepository;
            _loggedInUserService = loggedInUserService;
        }

        public async Task<RetornoService> Handle(CadastrarAutorCommand request, CancellationToken cancellationToken)
        {
            try
            {
                request.Id = null;
                _response.AddValue(await _autorRepository.InsertAsync(request));

                _response.Sucesso = true;
                return _response;
            }
            catch (Exception ex)
            {
                _response.Sucesso = false;
                _response.AddNotification(new("Erro ao cadastrar Autor", ex.Message));
                return _response;
            }
        }
    }
}
