using Flunt.Notifications;
using LivrosAPI.Application.Contracts;
using LivrosAPI.Application.Contracts.Persistence.Repositories;
using LivrosAPI.Application.Models.Identity;
using LivrosAPI.Application.Models.Identity.Commands;
using LivrosAPI.Application.Responses;
using LivrosAPI.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace LivrosAPI.Identity.Services.Commands
{
    public class AtualizarUsuarioHandler : IRequestHandler<AtualizarUsuarioCommand, RetornoService>
    {
        private RetornoService response;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IRepository<Usuario> _usuarioRepository;
        private readonly ILoggedInUserService _loggedInUserService;

        public AtualizarUsuarioHandler(UserManager<ApplicationUser> userManager, IRepository<Usuario> usuarioRepository, ILoggedInUserService loggedInUserService)
        {
            _userManager = userManager;
            response = new RetornoService();
            _usuarioRepository = usuarioRepository;
            _loggedInUserService = loggedInUserService;
        }

        public async Task<RetornoService> Handle(AtualizarUsuarioCommand request, CancellationToken cancellationToken)
        {
            Validate(request);

            if (response.TemMensagens)
            {
                response.Sucesso = false;
                return response;
            }

            var apsnetuser = await _userManager.FindByNameAsync(request.Email);
            IList<string> rolesAnteriores = await _userManager.GetRolesAsync(apsnetuser);
            Usuario usuario = await _usuarioRepository.GetAsync(a => a.IdAspNetUsers == apsnetuser.Id);
            usuario.Nome = request.Nome;           

            try
            {
                //update email and phone
                await _userManager.SetEmailAsync(apsnetuser, request.Email);
                await _userManager.SetUserNameAsync(apsnetuser, request.Email);
                await _userManager.SetPhoneNumberAsync(apsnetuser, request.PhoneNumber);

                //update roles, removendo a que existe e adicionando a que está sendo trazida
                //removendo
                await _userManager.RemoveFromRolesAsync(apsnetuser, rolesAnteriores);

                //adicionando
                await _userManager.AddToRolesAsync(apsnetuser, request.Roles);

                //update usuario
                await _usuarioRepository.UpdateAsync(usuario);

                response.Sucesso = true;
                return response;
            }
            catch (Exception ex)
            {
                response.Sucesso = false;
                var notificacao = new Notification("Atualizar usuário", ex.Message);
                response.AddNotification(notificacao);
                return response;
            }
        }
        private void Validate(AtualizarUsuarioCommand request)
        {
            if (string.IsNullOrEmpty(request.Email))
            {
                var notification = new Notification("email", "O campo de email é obrigatório");
                response.AddNotification(notification);

                if (!response.EmailValido(request.Email))
                {
                    notification = new Notification("email", "O campo de email está inválido");
                    response.AddNotification(notification);
                }
            }

        }
    }
}
