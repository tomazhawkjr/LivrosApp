using LivrosAPI.Application.Responses;
using MediatR;

namespace LivrosAPI.Application.Models.Identity.Commands
{
    public class CadastrarRoleCommand : IRequest<RetornoService>
    {
        public string? Name { get; set; }
    }
}
