using LivrosAPI.Application.Responses;
using MediatR;

namespace LivrosAPI.Application.Features.Autor.Commands.AtualizarAutor
{
    public class AtualizarAutorCommand : Domain.Entities.Autor, IRequest<RetornoService>
    {
    }
}