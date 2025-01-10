using LivrosAPI.Application.Responses;

namespace LivrosAPI.Application.Models.JsonToken
{
    public class JsonWebToken
    {
        public string? AccessToken { get; set; }
        public RefreshToken? RefreshToken { get; set; }
        public string TokenType { get; set; } = "bearer";
        public long ExpiresIn { get; set; }
    }
}
