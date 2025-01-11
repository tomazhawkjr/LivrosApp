using LivrosAPI.Domain.Reports.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LivrosAPI.Domain.Models.Dtos
{
    public class LivroByAutorDto : ILivroReportDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public List<LivroDtoByAutorLivro> Livros { get; set; }
        
    }

    public class LivroDtoByAutorLivro
    {
        public string Titulo { get; set; }
        public string Editora { get; set; }
        public int Edicao { get; set; }
        public string AnoPublicacao { get; set; }
        public DateTime DataCriacao { get; set; }
        public List<LivroDtoAssunto> Assuntos { get; set; }
        public List<LivroDtoValor> Valores { get; set; }
    }

}
