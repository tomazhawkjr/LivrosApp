using LivrosAPI.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LivrosAPI.Domain.Entities
{
    public class LivroValor : EntidadeRelacionamento
    {
        [JsonIgnore]
        public int? IdLivro { get; set; } = null;
        public int IdFormaCompra { get; set; }
        public decimal Valor { get; set; }

        [JsonIgnore]
        public FormaCompra? FormaCompra { get; set; }
        [JsonIgnore]
        public Livro? Livro { get; set; }
    }

}
