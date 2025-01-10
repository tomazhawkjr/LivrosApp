using LivrosAPI.Application.Responses;
using MediatR;

namespace LivrosAPI.Application.Models.Identity.Commands
{
    public class LoginCommand : IRequest<RetornoService>
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? GrantType { get; set; }
        public string? ClientId { get; set; }
        public string? RefreshToken { get; set; }
    }
}
