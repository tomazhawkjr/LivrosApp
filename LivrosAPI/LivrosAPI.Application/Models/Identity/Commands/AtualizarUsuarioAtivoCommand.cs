using LivrosAPI.Application.Responses;
using MediatR;

namespace LivrosAPI.Application.Models.Identity.Commands
{
    public class AtualizarUsuarioAtivoCommand : IRequest<RetornoService>
    {
        public int Id { get; set; }
        public bool Ativo { get; set; }
    }
}
