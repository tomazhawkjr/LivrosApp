using Flunt.Notifications;
using LivrosAPI.Application.Contracts;
using LivrosAPI.Application.Contracts.Infrastructure.Interfaces;
using LivrosAPI.Application.Contracts.Persistence.Repositories;
using LivrosAPI.Application.Responses;
using MediatR;

namespace LivrosAPI.Application.Features.Autor.Commands.DeletarAutor
{
    public class DeletarAutorHandler : IRequestHandlerBase<DeletarAutorCommand>
    {
        private readonly RetornoService _response;
        private readonly IAutorRepository _autorRepository;

        public DeletarAutorHandler(IAutorRepository autorRepository)
        {
            _response = new RetornoService();
            _autorRepository = autorRepository;
        }

        public async Task<RetornoService> Handle(DeletarAutorCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _autorRepository.DeleteAutor(request.Id);

                _response.Sucesso = true;
                return _response;
            }
            catch (Exception ex)
            {
                Notification notification = new Notification("Deletar", ex.Message);
                _response.Sucesso = false;
                _response.AddNotification(notification);
                return _response;
            }
        }
    }
}
