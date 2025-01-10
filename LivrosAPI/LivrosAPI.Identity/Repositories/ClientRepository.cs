using LivrosAPI.Application.Models.Application;
using LivrosAPI.Identity.Repositories.Interfaces;

namespace LivrosAPI.Identity.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly ApplicationDbContext _context;

        public ClientRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Client> Get(string clientId)
        {
            var client = _context.Clients
                .FirstOrDefault(a => a.Id.Equals(clientId));

            return await Task.FromResult(client);
        }
    }
}
