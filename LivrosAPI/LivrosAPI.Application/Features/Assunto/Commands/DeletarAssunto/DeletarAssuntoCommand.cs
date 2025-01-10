using LivrosAPI.Application.Responses;
using MediatR;

namespace LivrosAPI.Application.Features.Assunto.Commands.DeletarAssunto
{
    public class DeletarAssuntoCommand : IRequest<RetornoService>
    {
        public int Id { get; set; }
    }
}