using LivrosAPI.Application.Responses;
using MediatR;

namespace LivrosAPI.Application.Features.Autor.Commands.CadastrarAutor
{
    public class CadastrarAutorCommand : Domain.Entities.Autor, IRequest<RetornoService>
    {
    }
}

