using Flunt.Notifications;
using LivrosAPI.Application.Contracts.Persistence.Repositories;
using LivrosAPI.Application.Responses;
using LivrosAPI.Domain.Models.Dtos;
using MediatR;

namespace LivrosAPI.Application.Features.Autor.Queries.BuscarAutor
{
    public class BuscarAutorHandler : IRequestHandler<BuscarAutorQuery, RetornoService>
    {
        private readonly RetornoService _response;
        private readonly IAutorRepository _autorRepository;

        public BuscarAutorHandler(IAutorRepository autorRepository)
        {
            _response = new RetornoService();
            _autorRepository = autorRepository;
        }

        public async Task<RetornoService> Handle(BuscarAutorQuery request, CancellationToken cancellationToken)
        {
            try
            {                     
                List<Domain.Entities.Autor> list = await _autorRepository.GetAllAsync();

                _response.Sucesso = true;
                _response.AddValue(list);

                return _response;
            }
            catch (Exception ex)
            {                
                _response.Sucesso = false;
                _response.AddNotification(new("Erro ao buscar Autores", ex.Message));
                return _response;
            }
        }
    }
}
