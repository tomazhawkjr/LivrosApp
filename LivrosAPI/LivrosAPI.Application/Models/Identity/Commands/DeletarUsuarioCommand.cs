using LivrosAPI.Application.Responses;
using MediatR;

namespace LivrosAPI.Application.Models.Identity.Commands
{
    public class DeletarUsuarioCommand : IRequest<RetornoService>
    {
        public int Id { get; set; }
    }
}
