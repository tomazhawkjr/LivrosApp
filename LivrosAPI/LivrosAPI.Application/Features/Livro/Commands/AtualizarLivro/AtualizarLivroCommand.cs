using LivrosAPI.Application.Responses;
using MediatR;

namespace LivrosAPI.Application.Features.Livro.Commands.AtualizarLivro
{
    public class AtualizarLivroCommand : Domain.Entities.Livro, IRequest<RetornoService>
    {
    }
}