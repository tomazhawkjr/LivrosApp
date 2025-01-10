using LivrosAPI.Application.Contracts.Persistence.Repositories;
using LivrosAPI.Application.Features.Livro.Queries.BuscarLivro;
using LivrosAPI.Domain.Models.Dtos;
using Moq;

namespace LivrosAPI.Tests
{
    public class LivroApplicationTest
    {
        private readonly Mock<ILivroRepository> _livroRepositoryMock;
        private readonly BuscarLivroHandler _buscarLivroHandler;

        public LivroApplicationTest()
        {
            
            _livroRepositoryMock = new Mock<ILivroRepository>();
            _buscarLivroHandler = new BuscarLivroHandler(_livroRepositoryMock.Object);
        }

        [Fact]
        public async Task BuscaLivroQuerySucesso()
        {
            string titulo1 = "Livro 1";
            string titulo2 = "Livro 2";

            var livros = new List<LivroDto>
            {
                new LivroDto { Id = 1, Titulo = titulo1},
                new LivroDto { Id = 2, Titulo = titulo2}
            };
          
            _livroRepositoryMock
                .Setup(repo => repo.ListarLivros())
                .ReturnsAsync(livros);

            var query = new BuscarLivroQuery();
           
            var result = await _buscarLivroHandler.Handle(query, CancellationToken.None);
          
            Assert.True(result.Sucesso);
            Assert.NotNull(result.Value);
            Assert.IsType<List<LivroDto>>(result.Value);

            var listaLivros = (List<LivroDto>)result.Value;

            Assert.Equal(2, listaLivros.Count);
            Assert.Equal(titulo1, listaLivros[0].Titulo);
            Assert.Equal(titulo2, listaLivros[1].Titulo);
        }

        [Fact]
        public async Task BuscaLivroQueryErro()
        {            
            _livroRepositoryMock
                .Setup(repo => repo.ListarLivros())
                .ThrowsAsync(new Exception("Erro ao acessar o banco de dados"));
           
            var query = new BuscarLivroQuery();

            var result = await _buscarLivroHandler.Handle(query, CancellationToken.None);

            Assert.False(result.Sucesso);
            Assert.NotNull(result.ListaMensagem);
            Assert.Single(result.ListaMensagem);
            Assert.Equal("Erro ao acessar o banco de dados", result.ListaMensagem[0].Message);
        }
    }
}