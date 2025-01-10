using LivrosAPI.Domain.Entities.Base;

namespace LivrosAPI.Application.Models.Identity
{
    public class RoleVM : Entidade
    {
        public string Id { get; set; }
        public string? Name { get; set; }
        public string? NormalizedName { get; set; }
    }
}
