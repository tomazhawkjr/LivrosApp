using LivrosAPI.Application.Contracts;
using LivrosAPI.Application.Contracts.Persistence.Repositories;
using LivrosAPI.Application.Responses;
using MediatR;

namespace LivrosAPI.Application.Features.FormaCompra.Commands.CadastrarFormaCompra
{
    public class CadastrarFormaCompraHandler : IRequestHandler<CadastrarFormaCompraCommand, RetornoService>
    {
        private readonly RetornoService _response;
        private readonly IRepository<Domain.Entities.FormaCompra> _autorRepository;
        private readonly ILoggedInUserService _loggedInUserService;

        public CadastrarFormaCompraHandler(IRepository<Domain.Entities.FormaCompra> livroRepository, ILoggedInUserService loggedInUserService)
        {
            _response = new RetornoService();
            _autorRepository = livroRepository;
            _loggedInUserService = loggedInUserService;
        }

        public async Task<RetornoService> Handle(CadastrarFormaCompraCommand request, CancellationToken cancellationToken)
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
                _response.AddNotification(new("Erro ao cadastrar FormaCompra", ex.Message));
                return _response;
            }
        }
    }
}
