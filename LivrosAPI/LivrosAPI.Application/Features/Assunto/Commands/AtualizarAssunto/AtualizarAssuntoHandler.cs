using LivrosAPI.Application.Contracts.Infrastructure.Interfaces;
using LivrosAPI.Application.Contracts.Persistence.Repositories;
using LivrosAPI.Application.Responses;
using AssuntoEntidade = LivrosAPI.Domain.Entities.Assunto;

namespace LivrosAPI.Application.Features.Assunto.Commands.AtualizarAssunto
{
    public class AtualizarAssuntoHandler : IRequestHandlerBase<AtualizarAssuntoCommand>
    {
        private readonly RetornoService _response;
        private readonly IRepository<AssuntoEntidade> _autorRepository;

        public AtualizarAssuntoHandler(IRepository<AssuntoEntidade> autorRepository)
        {
            _response = new RetornoService();
            _autorRepository = autorRepository;
        }

        public async Task<RetornoService> Handle(AtualizarAssuntoCommand request, CancellationToken cancellationToken)
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
                _response.AddNotification(new("Atualizar Assunto", ex.Message));
                return _response;
            }
        }
    }
}
