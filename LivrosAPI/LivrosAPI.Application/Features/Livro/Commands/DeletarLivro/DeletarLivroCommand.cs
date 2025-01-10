using LivrosAPI.Application.Responses;
using MediatR;

namespace LivrosAPI.Application.Features.Livro.Commands.DeletarLivro
{
    public class DeletarLivroCommand : IRequest<RetornoService>
    {
        public int Id { get; set; }
    }
}