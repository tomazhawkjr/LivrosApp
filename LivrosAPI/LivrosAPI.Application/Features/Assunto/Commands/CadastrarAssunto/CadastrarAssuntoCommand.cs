using LivrosAPI.Application.Responses;
using MediatR;

namespace LivrosAPI.Application.Features.Assunto.Commands.CadastrarAssunto
{
    public class CadastrarAssuntoCommand : Domain.Entities.Assunto, IRequest<RetornoService>
    {
    }
}

