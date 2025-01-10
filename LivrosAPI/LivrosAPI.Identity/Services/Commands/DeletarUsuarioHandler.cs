using Flunt.Notifications;
using LivrosAPI.Application.Contracts;
using LivrosAPI.Application.Contracts.Infrastructure.Interfaces;
using LivrosAPI.Application.Contracts.Persistence.Repositories;
using LivrosAPI.Application.Models.Identity;
using LivrosAPI.Application.Models.Identity.Commands;
using LivrosAPI.Application.Responses;
using LivrosAPI.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace LivrosAPI.Identity.Services.Commands
{
    public class DeletarUsuarioHandler : IRequestHandlerBase<DeletarUsuarioCommand>
    {
        private RetornoService response;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IRepository<Usuario> _usuarioRepository;
        private readonly ILoggedInUserService _loggedInUserService;
        public DeletarUsuarioHandler(UserManager<ApplicationUser> userManager, IRepository<Usuario> usuarioRepository, ILoggedInUserService loggedInUserService)
        {
            _userManager = userManager;
            response = new RetornoService();
            _usuarioRepository = usuarioRepository;
            _loggedInUserService = loggedInUserService;
        }
        public async Task<RetornoService> Handle(DeletarUsuarioCommand request, CancellationToken cancellationToken)
        {

            try
            {
                Usuario usuario = await _usuarioRepository.GetAsync(request.Id);
                var aspnetuser = await _userManager.FindByIdAsync(usuario.IdAspNetUsers);
                IList<string> roles = await _userManager.GetRolesAsync(aspnetuser);

                await _usuarioRepository.DeleteAsync(usuario);

                //remover roles
                await _userManager.RemoveFromRolesAsync(aspnetuser, roles);

                //remover de aspnetusers
                await _userManager.DeleteAsync(aspnetuser);

                response.Sucesso = true;
                return response;
            }
            catch (Exception ex)
            {
                response.Sucesso = false;
                var notificacao = new Notification("error", ex.Message);
                response.AddNotification(notificacao);
                return response;
            }
        }
    }
}
