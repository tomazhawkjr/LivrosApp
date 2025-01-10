using System.ComponentModel.DataAnnotations;


namespace LivrosAPI.Application.Responses
{
    public class RefreshToken
    {
        [Key]
        public string? Token { get; set; }
        [Required]
        [MaxLength(50)]
        public string? UserName { get; set; }
        [Required]
        [MaxLength(50)]
        public string? ClientId { get; set; }
        public DateTime IssuedUtc { get; set; }
        public DateTime ExpiresUtc { get; set; }
    }
}
