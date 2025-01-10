using LivrosAPI.Application.Contracts.Jwt;
using LivrosAPI.Application.Models.Identity;
using LivrosAPI.Application.Models.JsonToken;
using LivrosAPI.Application.Models.Jwt;
using LivrosAPI.Application.Responses;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Principal;

namespace LivrosAPI.Infrastructure.Services.Jwt
{
    public class JwtService : IJwtService
    {
        private readonly JwtSettings _settings;

        public JwtService(JwtSettings settings)
        {
            _settings = settings;
        }

        public JsonWebToken CreateJsonWebToken(ApplicationUser user, IList<string> roles, int? idPerfil = null)
        {
            var identity = GetClaimsIdentity(user, roles, idPerfil);
            var handler = new JwtSecurityTokenHandler();
            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Subject = identity,
                Issuer = _settings.Issuer,
                Audience = _settings.Audience,
                IssuedAt = _settings.IssuedAt,
                NotBefore = _settings.NotBefore,
                Expires = _settings.AccessTokenExpiration,
                SigningCredentials = _settings.SigningCredentials
            });

            var accessToken = handler.WriteToken(securityToken);

            return new JsonWebToken
            {
                AccessToken = accessToken,
                RefreshToken = CreateRefreshToken(user.Email),
                ExpiresIn = (long)TimeSpan.FromMinutes(_settings.ValidForMinutes).TotalSeconds
            };
        }

        private RefreshToken CreateRefreshToken(string username)
        {
            var refreshToken = new RefreshToken
            {
                UserName = username,
                ExpiresUtc = _settings.RefreshTokenExpiration
            };

            HashAlgorithm hashAlgorithm = new SHA256CryptoServiceProvider();
            byte[] byteValue = System.Text.Encoding.UTF8.GetBytes(Guid.NewGuid().ToString("N"));
            byte[] byteHash = hashAlgorithm.ComputeHash(byteValue);
            refreshToken.Token = Convert.ToBase64String(byteHash);

            return refreshToken;
        }

        private static ClaimsIdentity GetClaimsIdentity(ApplicationUser user, IList<string> roles, int? idPerfil = null)
        {
            var identity = new ClaimsIdentity
            (
                new GenericIdentity(user.Email),
                new[] {
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                    new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                    new Claim("cargos", JsonConvert.SerializeObject(roles)),
                    new Claim("user_id", user.Id),
                    new Claim("perfil_id", idPerfil != null ? idPerfil.ToString() : string.Empty)
                }
            );

            if (roles != null && roles.Count > 0)
            {
                foreach (var role in roles)
                {
                    var roleClaim = new Claim(ClaimTypes.Role, role);
                    identity.AddClaim(roleClaim);
                }
            }

            return identity;
        }

    }
}
