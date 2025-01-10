using LivrosAPI.Domain.Entities;

namespace LivrosAPI.Application.Contracts
{
    public interface ILoggedInUserService
    {
        public Task<Usuario> GetUsuarioLogado();
    }
}
