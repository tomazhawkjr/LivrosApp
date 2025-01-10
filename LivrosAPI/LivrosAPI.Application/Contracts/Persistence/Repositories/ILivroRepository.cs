using LivrosAPI.Domain.Entities;
using LivrosAPI.Domain.Entities.Views;
using LivrosAPI.Domain.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LivrosAPI.Application.Contracts.Persistence.Repositories
{
    public interface ILivroRepository : IRepository<Livro>
    {
        public Task<List<LivroDto>> ListarLivros();
        public Task UpdateLivro(Livro livro);
        public Task DeleteLivro(int id);
    }
}
