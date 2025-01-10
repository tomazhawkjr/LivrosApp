using LivrosAPI.Application.Models.Identity;
using LivrosAPI.Identity.Repositories.Interfaces;

namespace LivrosAPI.Identity.Repositories
{
    public class ApplicationUserRepository : IApplicationUserRepository
    {
        private readonly ApplicationDbContext _context;

        public ApplicationUserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ApplicationUser>> GetUsers()
        {
            return await Task.FromResult(_context.Users);
        }

        public async Task<ApplicationUser> Get(Guid id)
        {
            var user = _context.Users
                .FirstOrDefault(a => a.Id.Equals(id));

            return await Task.FromResult(user);
        }

        public async Task<ApplicationUser> Get(string email)
        {
            var user = _context.Users
                .FirstOrDefault(a => a.Email.Equals(email));

            return await Task.FromResult(user);
        }

        public async Task<bool> ExistsUser(string email)
        {
            var exists = await Get(email) != null;
            return await Task.FromResult(exists);
        }

        public async Task Save(ApplicationUser user)
        {
            _context.Users.Add(user);

            await Task.CompletedTask;
        }

        public async Task Remove(ApplicationUser user)
        {
            _context.Users.Remove(user);

            await Task.CompletedTask;
        }
    }
}
