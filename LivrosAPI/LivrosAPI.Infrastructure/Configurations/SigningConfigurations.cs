using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;

namespace LivrosAPI.Infrastructure.Configurations
{
    public class SigningConfigurations
    {
        private static SigningConfigurations _instance;
        private static readonly object _padlock = new object();

        public SecurityKey Key { get; }
        public SigningCredentials SigningCredentials { get; }

        private SigningConfigurations()
        {
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
                using (var rsa = RSA.Create(2048)) // Generate the key
                {
                    Key = new RsaSecurityKey(rsa.ExportParameters(true));
                    File.WriteAllText("rsa_key.xml", rsa.ToXmlString(true)); // Persist the key
                }
            }

            SigningCredentials = new SigningCredentials(Key, SecurityAlgorithms.RsaSha256Signature);
        }

        public static SigningConfigurations Instance
        {
            get
            {
                lock (_padlock)
                {
                    return _instance ??= new SigningConfigurations();
                }
            }
        }
    }
}
