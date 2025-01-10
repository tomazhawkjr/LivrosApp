using Azure.Core;
using Dapper;
using LivrosAPI.Application.Contracts.Persistence.Repositories;
using LivrosAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LivrosAPI.Persistence.Repositories
{
    public class FormaCompraRepository : Repository<FormaCompra>, IFormaCompraRepository
    {

        public FormaCompraRepository(BDContext context) : base(context)
        {
        }

        public async Task DeleteFormaCompra(int id)
        {
            var formaCompraAtual = await _context.FormaCompras
           .AsNoTracking()
           .Include(l => l.LivroValores)
           .FirstOrDefaultAsync(l => l.Id == id);

            if (formaCompraAtual is not null)
            {
                if(formaCompraAtual.LivroValores is not null)
                    _context.LivroValores.RemoveRange(formaCompraAtual.LivroValores);    
                
                await DeleteAsync(formaCompraAtual);
            }           
        }

    }
}
