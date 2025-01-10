using LivrosAPI.Application.Contracts.Infrastructure.Interfaces;
using LivrosAPI.Application.Contracts.Persistence.Repositories;
using LivrosAPI.Application.Responses;
using FormaCompraEntidade = LivrosAPI.Domain.Entities.FormaCompra;

namespace LivrosAPI.Application.Features.FormaCompra.Commands.AtualizarFormaCompra
{
    public class AtualizarFormaCompraHandler : IRequestHandlerBase<AtualizarFormaCompraCommand>
    {
        private readonly RetornoService _response;
        private readonly IRepository<FormaCompraEntidade> _formaCompraRepository;

        public AtualizarFormaCompraHandler(IRepository<FormaCompraEntidade> formaCompraRepository)
        {
            _response = new RetornoService();
            _formaCompraRepository = formaCompraRepository;
        }

        public async Task<RetornoService> Handle(AtualizarFormaCompraCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _formaCompraRepository.UpdateAsync(request);
                _response.Sucesso = true;
                return _response;
            }
            catch (Exception ex)
            {               
                _response.Sucesso = false;
                _response.AddNotification(new("Atualizar FormaCompra", ex.Message));
                return _response;
            }
        }
    }
}
