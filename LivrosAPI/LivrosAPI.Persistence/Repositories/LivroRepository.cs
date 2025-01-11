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
    public class LivroRepository : Repository<Livro>, ILivroRepository
    {

        public LivroRepository(BDContext context) : base(context)
        {
        }

        public async Task DeleteLivro(int id)
        {
            var livroAtual = await _context.Livros
           .AsNoTracking()
           .Include(l => l.LivroAutores)
           .Include(l => l.LivroAssuntos)
           .Include(l => l.LivroValores)
           .FirstOrDefaultAsync(l => l.Id == id);

            if (livroAtual is not null)
            {
                if(livroAtual.LivroAutores is not null)
                    _context.LivroAutores.RemoveRange(livroAtual.LivroAutores);
                if (livroAtual.LivroAssuntos is not null)
                    _context.LivroAssuntos.RemoveRange(livroAtual.LivroAssuntos);
                if (livroAtual.LivroValores is not null)
                    _context.LivroValores.RemoveRange(livroAtual.LivroValores);
                await DeleteAsync(livroAtual);
            }           
        }

        public async Task<List<LivroDto>> ListarLivros()
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT * FROM vw_LivroDetalhado";
                var vwlivros = await connection.QueryAsync<vw_livroDetalhado>(query);

                List<LivroDto> livrosDto = vwlivros
                                            .GroupBy(l => l.IdLivro) 
                                            .Select(g => new LivroDto
                                            {
                                                Id = g.Key, 
                                                Titulo = g.FirstOrDefault().Titulo,
                                                Editora = g.FirstOrDefault().Editora,
                                                Edicao = g.FirstOrDefault().Edicao,
                                                AnoPublicacao = g.FirstOrDefault().AnoPublicacao,
                                                DataCriacao = g.FirstOrDefault().DataCriacaoLivro,
                                                Assuntos = g.Where(l => l.IdLivro == g.Key).Select(l => new LivroDtoAssunto
                                                {
                                                    Id = l.IdAssunto,
                                                    Descricao = l.DescricaoAssunto
                                                }).DistinctBy(a => a.Id).ToList(),
                                                Autores = g.Where(l => l.IdLivro == g.Key).Select(l => new LivroDtoAutor
                                                {
                                                    Id = l.IdAutor,
                                                    Nome = l.NomeAutor
                                                }).DistinctBy(a => a.Id).ToList(),
                                                Valores = g.Where(l => l.IdLivro == g.Key).Select(l => new LivroDtoValor
                                                {
                                                    Valor = l.ValorLivro,
                                                    DenominacaoFormaCompra = l.DenominacaoFormaCompra,
                                                    IdFormaCompra = l.IdFormaCompra                                                    
                                                }).DistinctBy(a => a.DenominacaoFormaCompra).ToList()
                                            }).ToList();

                return livrosDto;
            }
        }

        public async Task<List<LivroByAutorDto>> ListarLivrosByAutor()
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT * FROM vw_LivroDetalhado";
                var vwlivros = await connection.QueryAsync<vw_livroDetalhado>(query);

                List<LivroByAutorDto> livrosDto = vwlivros
                                            .GroupBy(l => l.IdAutor)
                                            .Select(g => new LivroByAutorDto
                                            {
                                                Id = g.Key,
                                                Nome = g.FirstOrDefault().NomeAutor,
                                                Livros = g.Where(a => a.IdAutor == g.Key)
                                                .DistinctBy(a => a.IdLivro)
                                                .Select(l => new LivroDtoByAutorLivro
                                                {
                                                    Titulo = l.Titulo,
                                                    AnoPublicacao = l.AnoPublicacao,
                                                    DataCriacao = l.DataCriacaoLivro,
                                                    Edicao = l.Edicao,
                                                    Editora = l.Editora,
                                                    Valores = g.Where(a => a.IdLivro == l.IdLivro)
                                                    .DistinctBy(a => a.IdFormaCompra)
                                                    .Select(
                                                        a => new LivroDtoValor(){ 
                                                            DenominacaoFormaCompra = a.DenominacaoFormaCompra,
                                                            IdFormaCompra = a.IdFormaCompra,
                                                            Valor = a.ValorLivro
                                                        }    
                                                    ).ToList(),
                                                    Assuntos = g.Where(a => a.IdLivro == l.IdLivro)
                                                    .DistinctBy(a => a.IdAssunto)
                                                    .Select(
                                                        a => new LivroDtoAssunto()
                                                        {
                                                           Descricao = a.DescricaoAssunto,
                                                           Id = a.IdAssunto
                                                        }
                                                    ).ToList()
                                                }
                                                ).ToList()                                               
                                            }).ToList();

                return livrosDto;
            }
        }

        public async Task UpdateLivro(Livro livro)
        {
            var livroAtual = await _context.Livros
            .AsNoTracking()
            .Include(l => l.LivroAutores)
            .Include(l => l.LivroAssuntos)
            .Include(l => l.LivroValores)
            .FirstOrDefaultAsync(l => l.Id == livro.Id);

            if (livroAtual is not null) {
                _context.LivroAutores.RemoveRange(livroAtual.LivroAutores);
                _context.LivroAssuntos.RemoveRange(livroAtual.LivroAssuntos);
                _context.LivroValores.RemoveRange(livroAtual.LivroValores);
            }

            await _context.SaveChangesAsync();

            _context.Entry(livroAtual).State = EntityState.Detached;

            await UpdateAsync(livro);
        }
    }
}
