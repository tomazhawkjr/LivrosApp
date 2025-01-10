using Microsoft.AspNetCore.Identity;

namespace LivrosAPI.Application.Models.Identity
{
    public class ApplicationRole : IdentityRole
    {
        public virtual ICollection<ApplicationUserRole>? UserRoles { get; set; }
    }
}
