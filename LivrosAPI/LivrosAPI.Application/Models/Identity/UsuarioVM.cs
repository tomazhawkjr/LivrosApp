using LivrosAPI.Domain.Entities.Base;

namespace LivrosAPI.Application.Models.Identity
{
    public class UsuarioVM : Entidade
    {
        public string Nome { get; set; }
        public bool Ativo { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Perfil { get; set; }
    }
}
