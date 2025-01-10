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
    public class Assunto : Entidade
    {
        [Required]
        [MaxLength(20)]
        public string Descricao { get; set; }

        // Relacionamento inverso
        [JsonIgnore]
        public ICollection<Livro>? Livros { get; set; }
        [JsonIgnore]
        public ICollection<LivroAssunto>? LivroAssuntos { get; set; }
    }

}
