using LivrosAPI.Application.Contracts;
using LivrosAPI.Application.Contracts.Persistence.Repositories;
using LivrosAPI.Application.Models;
using LivrosAPI.Application.Models.Identity;
using LivrosAPI.Domain.Constants;
using LivrosAPI.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using System.Security.Claims;

namespace LivrosAPI.API.Services
{
    public class LoggedInUserService : ILoggedInUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRepository<Usuario> _usuarioRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMemoryCache _memoryCache;
        private readonly MySettings _mySettingsConfig;


        public LoggedInUserService(IHttpContextAccessor httpContextAccessor, 
            IRepository<Usuario> usuarioRepository, 
            UserManager<ApplicationUser> userManager, 
            IMemoryCache memoryCache,
            IOptions<MySettings> mySettingsConfig)
        {
            _httpContextAccessor = httpContextAccessor;
            _usuarioRepository = usuarioRepository;
            _userManager = userManager;
            _memoryCache = memoryCache;
            _mySettingsConfig = mySettingsConfig.Value;
        }

        public async Task<Usuario> GetUsuarioLogado()
        {
            Usuario usuario = default;

            var userIdentifier = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            string cacheKey = $"{Constants.CacheConfigs.LOGGEDIN_SERVICE_PREFIX}{userIdentifier}";

            int tempoExpiracaoCache = _mySettingsConfig.MemoryCacheTempoExpiracaoMinutos ?? 30;

            if (userIdentifier is not null)
            {
                if (!_memoryCache.TryGetValue(cacheKey, out usuario))
                {
                    ApplicationUser user = _userManager.FindByEmailAsync(userIdentifier).Result;
                    usuario = await _usuarioRepository.GetAsync(a => a.IdAspNetUsers == user.Id);

                    var cacheOptions = new MemoryCacheEntryOptions
                    {
                        AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(tempoExpiracaoCache),
                        SlidingExpiration = TimeSpan.FromMinutes(tempoExpiracaoCache)
                    };

                    _memoryCache.Set(cacheKey, usuario, cacheOptions);
                }

            }

            return usuario;
        }     
    }
}
