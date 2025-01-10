using LivrosAPI.Application.Models.Application;

namespace LivrosAPI.Identity.Repositories.Interfaces
{
    public interface IClientRepository
    {
        Task<Client> Get(string clientId);
    }
}
