using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace LivrosAPI.Application.Models.Jwt
{
    public class JwtSettings
    {
        public string Audience { get; set; } // Changed to settable
        public string Issuer { get; set; } // Changed to settable
        public int ValidForMinutes { get; set; } // Changed to settable
        public int RefreshTokenValidForMinutes { get; set; } // Changed to settable
        public SigningCredentials SigningCredentials { get; set; } // Changed to settable

        public DateTime IssuedAt => DateTime.UtcNow;
        public DateTime NotBefore => IssuedAt;
        public DateTime AccessTokenExpiration => IssuedAt.AddMinutes(ValidForMinutes);
        public DateTime RefreshTokenExpiration => IssuedAt.AddMinutes(RefreshTokenValidForMinutes);

        // Parameterless constructor
        public JwtSettings() { }

        // Existing constructor can remain if needed
        public JwtSettings(IConfiguration configuration)
        {
            Issuer = configuration["JwtSettings:Issuer"];
            Audience = configuration["JwtSettings:Audience"];
            ValidForMinutes = Convert.ToInt32(configuration["JwtSettings:ValidForMinutes"]);
            RefreshTokenValidForMinutes = Convert.ToInt32(configuration["JwtSettings:RefreshTokenValidForMinutes"]);
            SigningCredentials = SigningConfigurations.Instance.SigningCredentials;
        }

    }
}
