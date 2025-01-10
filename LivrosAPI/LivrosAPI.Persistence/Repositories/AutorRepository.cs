using Azure.Core;
using Dapper;
using LivrosAPI.Application.Contracts.Persistence.Repositories;
using LivrosAPI.Domain.Entities;
using LivrosAPI.Domain.Entities.Views;
using LivrosAPI.Domain.Models.Dtos;
using LivrosAPI.Persistence;
using LivrosAPI.Persistence.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace LivrosAPI.Persistence.Repositories
{
    public class AutorRepository : Repository<Autor>, IAutorRepository
    {

        public AutorRepository(BDContext context) : base(context)
        {
        }

        public async Task DeleteAutor(int id)
        {
            var autorAtual = await _context.Autores
           .AsNoTracking()
           .Include(l => l.LivroAutores)
           .FirstOrDefaultAsync(l => l.Id == id);

            if (autorAtual is not null)
            {
                if(autorAtual.LivroAutores is not null)
                    _context.LivroAutores.RemoveRange(autorAtual.LivroAutores);               
                await DeleteAsync(autorAtual);
            }           
        }

    }
}
