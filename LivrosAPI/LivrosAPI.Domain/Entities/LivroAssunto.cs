using LivrosAPI.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LivrosAPI.Domain.Entities
{
    public class LivroAssunto : EntidadeRelacionamento
    {
        [JsonIgnore]
        public int? IdLivro { get; set; } = null;
        [JsonIgnore]
        public Livro? Livro { get; set; }

        public int IdAssunto { get; set; }
        [JsonIgnore]
        public Assunto? Assunto { get; set; }      
    }

}
