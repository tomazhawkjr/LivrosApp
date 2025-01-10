using LivrosAPI.Domain.Entities.Base;

namespace LivrosAPI.Domain.Entities
{
    public class Usuario : Entidade
    {
        public string IdAspNetUsers { get; set; }
        public string Nome { get; set; }
        public bool Ativo { get; set; }
       
    }
}
