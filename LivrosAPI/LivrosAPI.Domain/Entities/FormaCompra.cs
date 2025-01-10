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
    public class FormaCompra : Entidade
    {        
        [Required]
        [MaxLength(40)]
        public string Denominacao { get; set; }

        // Relacionamento com LivroValor (um-para-muitos)
        [JsonIgnore]
        public ICollection<LivroValor>? LivroValores { get; set; }
    }

}
