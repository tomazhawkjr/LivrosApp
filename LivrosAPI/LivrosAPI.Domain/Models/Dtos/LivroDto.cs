using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LivrosAPI.Domain.Models.Dtos
{
    public class LivroDto
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Editora { get; set; }
        public int Edicao { get; set; }
        public string AnoPublicacao { get; set; }
        public DateTime DataCriacao { get; set; }
        public List<LivroDtoAssunto> Assuntos { get; set; }
        public List<LivroDtoAutor> Autores { get; set; }
        public List<LivroDtoValor> Valores { get; set; }
    }

    public class LivroDtoAssunto
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
    }

    public class LivroDtoAutor
    {
        public int Id { get; set; }
        public string Nome { get; set; }
    }

    public class LivroDtoValor
    {
        public decimal Valor { get; set; }
        public int IdFormaCompra { get; set; }
        public string DenominacaoFormaCompra { get; set; }
    }

  
}
