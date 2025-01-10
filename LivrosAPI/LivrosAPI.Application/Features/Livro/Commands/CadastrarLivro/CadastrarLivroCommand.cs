using LivrosAPI.Application.Responses;
using MediatR;

namespace LivrosAPI.Application.Features.Livro.Commands.CadastrarLivro
{
    public class CadastrarLivroCommand : Domain.Entities.Livro, IRequest<RetornoService>
    {
    }
}

