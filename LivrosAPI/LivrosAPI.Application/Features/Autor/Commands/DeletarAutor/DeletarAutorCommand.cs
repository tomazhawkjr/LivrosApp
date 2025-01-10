using LivrosAPI.Application.Responses;
using MediatR;

namespace LivrosAPI.Application.Features.Autor.Commands.DeletarAutor
{
    public class DeletarAutorCommand : IRequest<RetornoService>
    {
        public int Id { get; set; }
    }
}