using LivrosAPI.Application.Responses;
using MediatR;

namespace LivrosAPI.Application.Features.Assunto.Commands.AtualizarAssunto
{
    public class AtualizarAssuntoCommand : Domain.Entities.Assunto, IRequest<RetornoService>
    {
    }
}