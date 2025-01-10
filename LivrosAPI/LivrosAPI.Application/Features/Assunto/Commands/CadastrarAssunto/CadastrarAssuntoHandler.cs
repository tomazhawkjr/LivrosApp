using LivrosAPI.Application.Contracts;
using LivrosAPI.Application.Contracts.Persistence.Repositories;
using LivrosAPI.Application.Responses;
using MediatR;

namespace LivrosAPI.Application.Features.Assunto.Commands.CadastrarAssunto
{
    public class CadastrarAssuntoHandler : IRequestHandler<CadastrarAssuntoCommand, RetornoService>
    {
        private readonly RetornoService _response;
        private readonly IRepository<Domain.Entities.Assunto> _autorRepository;
        private readonly ILoggedInUserService _loggedInUserService;

        public CadastrarAssuntoHandler(IRepository<Domain.Entities.Assunto> livroRepository, ILoggedInUserService loggedInUserService)
        {
            _response = new RetornoService();
            _autorRepository = livroRepository;
            _loggedInUserService = loggedInUserService;
        }

        public async Task<RetornoService> Handle(CadastrarAssuntoCommand request, CancellationToken cancellationToken)
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
                _response.AddNotification(new("Erro ao cadastrar Assunto", ex.Message));
                return _response;
            }
        }
    }
}
