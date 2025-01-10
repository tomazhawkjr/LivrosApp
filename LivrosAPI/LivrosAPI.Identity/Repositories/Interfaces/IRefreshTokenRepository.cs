using LivrosAPI.Application.Responses;

namespace LivrosAPI.Identity.Repositories.Interfaces
{
    public interface IRefreshTokenRepository
    {
        Task<RefreshToken> Get(string refreshToken);
        Task Save(RefreshToken refreshToken);
    }
}
