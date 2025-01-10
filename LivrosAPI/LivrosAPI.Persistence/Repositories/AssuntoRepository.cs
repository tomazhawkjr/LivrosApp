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
    public class AssuntoRepository : Repository<Assunto>, IAssuntoRepository
    {

        public AssuntoRepository(BDContext context) : base(context)
        {
        }

        public async Task DeleteAssunto(int id)
        {
            var assuntoAtual = await _context.Assuntos
           .AsNoTracking()
           .Include(l => l.LivroAssuntos)
           .FirstOrDefaultAsync(l => l.Id == id);

            if (assuntoAtual is not null)
            {
                if(assuntoAtual.LivroAssuntos is not null)
                    _context.LivroAssuntos.RemoveRange(assuntoAtual.LivroAssuntos);               
                await DeleteAsync(assuntoAtual);
            }           
        }

    }
}
