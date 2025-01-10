using LivrosAPI.Application.Contracts.Persistence.Repositories;
using LivrosAPI.Application.Responses;
using MediatR;

namespace LivrosAPI.Application.Features.FormaCompra.Queries.BuscarFormaCompra
{
    public class BuscarFormaCompraHandler : IRequestHandler<BuscarFormaCompraQuery, RetornoService>
    {
        private readonly RetornoService _response;
        private readonly IFormaCompraRepository _assuntoRepository;

        public BuscarFormaCompraHandler(IFormaCompraRepository assuntoRepository)
        {
            _response = new RetornoService();
            _assuntoRepository = assuntoRepository;
        }

        public async Task<RetornoService> Handle(BuscarFormaCompraQuery request, CancellationToken cancellationToken)
        {
            try
            {                     
                List<Domain.Entities.FormaCompra> list = await _assuntoRepository.GetAllAsync();

                _response.Sucesso = true;
                _response.AddValue(list);

                return _response;
            }
            catch (Exception ex)
            {                
                _response.Sucesso = false;
                _response.AddNotification(new("Erro ao buscar FormaCompras", ex.Message));
                return _response;
            }
        }
    }
}
