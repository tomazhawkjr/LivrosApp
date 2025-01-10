using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;

namespace LivrosAPI.Application.Models
{
    public class SigningConfigurations
    {
        private static SigningConfigurations _instance = null;
        private static readonly object _padlock = new object();

        public SecurityKey Key { get; }
        public SigningCredentials SigningCredentials { get; }

        private SigningConfigurations()
        {
            // Tenta carregar a chave de um arquivo, ou gera uma nova se não existir
            if (File.Exists("rsa_key.xml"))
            {
                using (var rsa = RSA.Create())
                {
                    rsa.FromXmlString(File.ReadAllText("rsa_key.xml"));
                    Key = new RsaSecurityKey(rsa.ExportParameters(true));
                }
            }
            else
            {
                using (var rsa = RSA.Create(2048)) // Gera a chave com 2048 bits
                {
                    Key = new RsaSecurityKey(rsa.ExportParameters(true));
                    File.WriteAllText("rsa_key.xml", rsa.ToXmlString(true)); // Persiste a chave
                }
            }

            SigningCredentials = new SigningCredentials(
                Key, SecurityAlgorithms.RsaSha256Signature);
        }

        // Singleton: Garante uma única instância para toda a aplicação
        public static SigningConfigurations Instance
        {
            get
            {
                lock (_padlock)
                {
                    if (_instance == null)
                    {
                        _instance = new SigningConfigurations();
                    }
                    return _instance;
                }
            }
        }
    }
}