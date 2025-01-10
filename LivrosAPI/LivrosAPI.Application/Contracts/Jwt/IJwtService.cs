using LivrosAPI.Application.Models.Identity;
using LivrosAPI.Application.Models.JsonToken;

namespace LivrosAPI.Application.Contracts.Jwt
{
    public interface IJwtService
    {
        JsonWebToken CreateJsonWebToken(ApplicationUser user, IList<string> roles, int? idPerfil = null);
    }
}
