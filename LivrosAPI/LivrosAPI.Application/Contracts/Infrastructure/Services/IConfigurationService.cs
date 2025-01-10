using LivrosAPI.Application.Models;

namespace LivrosAPI.Application.Contracts.Infrastructure.Services
{
    public interface IConfigurationService
    {

        public MySettings GetConfigs();      

    }
}
