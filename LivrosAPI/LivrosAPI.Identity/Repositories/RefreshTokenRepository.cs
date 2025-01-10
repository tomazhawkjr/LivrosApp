using LivrosAPI.Application.Responses;
using LivrosAPI.Identity.Repositories.Interfaces;

namespace LivrosAPI.Identity.Repositories
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly ApplicationDbContext _context;

        public RefreshTokenRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<RefreshToken> Get(string refreshToken)
        {
            var currentRefreshToken = _context.RefreshTokens
                .FirstOrDefault(a => a.Token.Equals(refreshToken));

            return await Task.FromResult(currentRefreshToken);
        }

        public async Task Save(RefreshToken refreshToken)
        {
            var currentRefreshToken = _context.RefreshTokens
                .FirstOrDefault(a => a.UserName.Equals(refreshToken.UserName));

            if (currentRefreshToken != null)
            {
                _context.RefreshTokens.Remove(currentRefreshToken);
            }

            try
            {
                _context.RefreshTokens.Add(refreshToken);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            await Task.CompletedTask;
        }
    }
}
