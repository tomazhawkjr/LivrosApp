using LivrosAPI.Application.Models.Identity;

namespace LivrosAPI.Identity.Repositories.Interfaces
{
    public interface IApplicationUserRepository
    {
        Task<bool> ExistsUser(string email);
        Task Save(ApplicationUser user);
        Task Remove(ApplicationUser user);
        Task<ApplicationUser> Get(Guid id);
        Task<ApplicationUser> Get(string email);
    }
}
