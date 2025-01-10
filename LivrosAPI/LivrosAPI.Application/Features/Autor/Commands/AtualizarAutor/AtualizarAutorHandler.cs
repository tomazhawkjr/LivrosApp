using LivrosAPI.Application.Contracts.Infrastructure.Interfaces;
using LivrosAPI.Application.Contracts.Persistence.Repositories;
using LivrosAPI.Application.Responses;
using AutorEntidade = LivrosAPI.Domain.Entities.Autor;

namespace LivrosAPI.Application.Features.Autor.Commands.AtualizarAutor
{
    public class AtualizarAutorHandler : IRequestHandlerBase<AtualizarAutorCommand>
    {
        private readonly RetornoService _response;
        private readonly IRepository<AutorEntidade> _autorRepository;

        public AtualizarAutorHandler(IRepository<AutorEntidade> autorRepository)
        {
            _response = new RetornoService();
            _autorRepository = autorRepository;
        }

        public async Task<RetornoService> Handle(AtualizarAutorCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _autorRepository.UpdateAsync(request);
                _response.Sucesso = true;
                return _response;
            }
            catch (Exception ex)
            {               
                _response.Sucesso = false;
                _response.AddNotification(new("Atualizar Autor", ex.Message));
                return _response;
            }
        }
    }
}
