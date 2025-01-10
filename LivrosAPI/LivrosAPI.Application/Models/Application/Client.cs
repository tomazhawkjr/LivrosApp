using System.ComponentModel.DataAnnotations;

namespace LivrosAPI.Application.Models.Application
{
    public class Client
    {
        [Key]
        public string? Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string? Name { get; set; }
        public bool Active { get; set; }
        public int RefreshTokenLifeTime { get; set; }
        [MaxLength(100)]
        public string? AllowedOrigin { get; set; }

    }

}
