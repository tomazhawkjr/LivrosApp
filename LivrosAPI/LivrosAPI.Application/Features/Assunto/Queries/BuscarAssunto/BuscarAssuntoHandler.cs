using Flunt.Notifications;
using LivrosAPI.Application.Contracts.Persistence.Repositories;
using LivrosAPI.Application.Responses;
using LivrosAPI.Domain.Models.Dtos;
using MediatR;

namespace LivrosAPI.Application.Features.Assunto.Queries.BuscarAssunto
{
    public class BuscarAssuntoHandler : IRequestHandler<BuscarAssuntoQuery, RetornoService>
    {
        private readonly RetornoService _response;
        private readonly IAssuntoRepository _assuntoRepository;

        public BuscarAssuntoHandler(IAssuntoRepository assuntoRepository)
        {
            _response = new RetornoService();
            _assuntoRepository = assuntoRepository;
        }

        public async Task<RetornoService> Handle(BuscarAssuntoQuery request, CancellationToken cancellationToken)
        {
            try
            {                     
                List<Domain.Entities.Assunto> list = await _assuntoRepository.GetAllAsync();

                _response.Sucesso = true;
                _response.AddValue(list);

                return _response;
            }
            catch (Exception ex)
            {                
                _response.Sucesso = false;
                _response.AddNotification(new("Erro ao buscar Assuntos", ex.Message));
                return _response;
            }
        }
    }
}
