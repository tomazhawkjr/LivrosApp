using LivrosAPI.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LivrosAPI.Domain.Entities
{
    public class Livro : Entidade
    {
        [Required]
        [MaxLength(40)]
        public string Titulo { get; set; }
        [Required]
        [MaxLength(40)]
        public string Editora { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "O valor deve ser maior que 0.")]
        public int Edicao { get; set; }
        [Required]
        [MaxLength(4)]
        public string AnoPublicacao { get; set; }

        // Relacionamentos
        [JsonIgnore]
        public ICollection<Autor>? Autores { get; set; }
        [JsonIgnore]
        public ICollection<Assunto>? Assuntos { get; set; }

        public ICollection<LivroAssunto> LivroAssuntos { get; set; }
        public ICollection<LivroAutor> LivroAutores { get; set; }
        public ICollection<LivroValor> LivroValores { get; set; }
    }

}
