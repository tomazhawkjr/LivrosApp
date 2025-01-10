using Flunt.Notifications;
using LivrosAPI.Application.Contracts;
using LivrosAPI.Application.Contracts.Infrastructure.Interfaces;
using LivrosAPI.Application.Contracts.Persistence.Repositories;
using LivrosAPI.Application.Responses;
using MediatR;

namespace LivrosAPI.Application.Features.FormaCompra.Commands.DeletarFormaCompra
{
    public class DeletarFormaCompraHandler : IRequestHandlerBase<DeletarFormaCompraCommand>
    {
        private readonly RetornoService _response;
        private readonly IFormaCompraRepository _assuntoRepository;

        public DeletarFormaCompraHandler(IFormaCompraRepository assuntoRepository)
        {
            _response = new RetornoService();
            _assuntoRepository = assuntoRepository;
        }

        public async Task<RetornoService> Handle(DeletarFormaCompraCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _assuntoRepository.DeleteFormaCompra(request.Id);

                _response.Sucesso = true;
                return _response;
            }
            catch (Exception ex)
            {
                _response.Sucesso = false;
                _response.AddNotification(new("Deletar FormaCompra", ex.Message));
                return _response;
            }
        }
    }
}
