using LivrosAPI.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LivrosAPI.Domain.Entities
{
    public class LivroAutor : EntidadeRelacionamento
    {
        [JsonIgnore]
        public int? IdLivro { get; set; } = null;
        [JsonIgnore]
        public Livro? Livro { get; set; }

        public int IdAutor { get; set; }
        [JsonIgnore]
        public Autor? Autor { get; set; }
    }

}
