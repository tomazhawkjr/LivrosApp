using LivrosAPI.Domain.Entities;

namespace LivrosAPI.Application.Contracts.Persistence.Repositories
{
    public interface IFormaCompraRepository : IRepository<FormaCompra>
    {
        public Task DeleteFormaCompra(int id);
    }
}
