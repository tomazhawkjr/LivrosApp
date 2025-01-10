using LivrosAPI.Application.Contracts.Infrastructure.Services;
using LivrosAPI.Application.Models;
using Microsoft.Extensions.Options;

namespace LivrosAPI.Infrastructure.Services
{
    public class ConfigurationService : IConfigurationService
    {

        private readonly IOptions<MySettings> _mySettingsConfig;      

        public ConfigurationService(IOptions<MySettings> config)
        {
            _mySettingsConfig = config;
        }

        public MySettings GetConfigs()
        {
            return _mySettingsConfig.Value;
        }
    }
}
