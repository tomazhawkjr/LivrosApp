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
    public class Autor : Entidade
    {
        [Required]
        [MaxLength(40)]
        public string Nome { get; set; }

        // Relacionamento inverso
        [JsonIgnore]
        public ICollection<Livro>? Livros { get; set; }
        [JsonIgnore]
        public ICollection<LivroAutor>? LivroAutores { get; set; }
    }

}
