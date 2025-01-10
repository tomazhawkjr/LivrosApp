using LivrosAPI.Application.Responses;
using MediatR;

namespace LivrosAPI.Application.Models.Identity.Commands
{
    public class AtualizarUsuarioCommand : IRequest<RetornoService>
    {
        public string Email { get; set; }
        public string Nome { get; set; }
        public string? PhoneNumber { get; set; }
        public IList<string> Roles { get; set; }
    }
}
