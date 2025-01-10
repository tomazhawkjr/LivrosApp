using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LivrosAPI.Domain.Entities.Base
{
    public class Entidade
    {
        public Entidade()
        {
            DataCriacao = DateTime.Now;
        }

        public int? Id { get; set; } = null;
        public DateTime? DataCriacao { get; set; }
    }
}
