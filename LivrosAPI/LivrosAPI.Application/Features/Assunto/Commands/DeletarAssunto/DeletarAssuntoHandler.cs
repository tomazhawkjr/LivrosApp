using Flunt.Notifications;
using LivrosAPI.Application.Contracts;
using LivrosAPI.Application.Contracts.Infrastructure.Interfaces;
using LivrosAPI.Application.Contracts.Persistence.Repositories;
using LivrosAPI.Application.Responses;
using MediatR;

namespace LivrosAPI.Application.Features.Assunto.Commands.DeletarAssunto
{
    public class DeletarAssuntoHandler : IRequestHandlerBase<DeletarAssuntoCommand>
    {
        private readonly RetornoService _response;
        private readonly IAssuntoRepository _assuntoRepository;

        public DeletarAssuntoHandler(IAssuntoRepository assuntoRepository)
        {
            _response = new RetornoService();
            _assuntoRepository = assuntoRepository;
        }

        public async Task<RetornoService> Handle(DeletarAssuntoCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _assuntoRepository.DeleteAssunto(request.Id);

                _response.Sucesso = true;
                return _response;
            }
            catch (Exception ex)
            {
                _response.Sucesso = false;
                _response.AddNotification(new("Deletar Assunto", ex.Message));
                return _response;
            }
        }
    }
}
