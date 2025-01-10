using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LivrosAPI.Domain.Entities.Base
{
    public class EntidadeRelacionamento
    {
        public EntidadeRelacionamento()
        {
            DataCriacao = DateTime.Now;
        }

        [JsonIgnore]
        public int? Id { get; set; } = null;
        [JsonIgnore]
        public DateTime? DataCriacao { get; set; }
    }
}
