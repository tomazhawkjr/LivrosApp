using LivrosAPI.Domain.Entities.Base;

namespace LivrosAPI.Domain.Entities.Views
{
    public class vw_livroDetalhado
    {
        public int IdLivro { get; set; }
        public string Titulo { get; set; }
        public string Editora { get; set; }
        public int Edicao { get; set; }
        public string AnoPublicacao { get; set; }
        public DateTime DataCriacaoLivro { get; set; }
        public int IdAutor { get; set; }
        public string NomeAutor { get; set; }
        public int IdAssunto { get; set; }
        public string DescricaoAssunto { get; set; }
        public decimal ValorLivro { get; set; }
        public int IdFormaCompra { get; set; }
        public string DenominacaoFormaCompra { get; set; }
    }
}
