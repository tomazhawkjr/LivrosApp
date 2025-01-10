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
    public class CadastrarUsuarioHandler : IRequestHandler<CadastrarUsuarioCommand, RetornoService>, IRequestHandler<Erro, RetornoService>
    {
        private RetornoService response;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IRepository<Usuario> _usuarioRepository;
        private readonly ILoggedInUserService _loggedInUserService;

        public CadastrarUsuarioHandler(UserManager<ApplicationUser> userManager, IRepository<Usuario> usuarioRepository, ILoggedInUserService loggedInUserService)
        {
            _userManager = userManager;
            response = new RetornoService();
            _usuarioRepository = usuarioRepository;
            _loggedInUserService = loggedInUserService;
        }

        public async Task<RetornoService> Handle(CadastrarUsuarioCommand request, CancellationToken cancellationToken)
        {
            response.Sucesso = true;

            Validate(request);

            if (response.TemMensagens)
            {
                response.Sucesso = false;
                return response;
            }

            var user = new ApplicationUser { UserName = request.Email, Email = request.Email, PhoneNumber = request.PhoneNumber };

            var result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
            {
                response.Sucesso = false;

                var listError = result.Errors;

                foreach (var error in listError)
                {
                    var notificacao = new Notification("error", error.Description);
                    response.AddNotification(notificacao);
                }
                return response;
            }

            ApplicationUser newUser = await _userManager.FindByNameAsync(request.UserName);

            result = await _userManager.AddToRolesAsync(newUser, request.Roles);

            if (!result.Succeeded)
            {
                response.Sucesso = false;

                var listError = result.Errors;

                foreach (var error in listError)
                {
                    var notificacao = new Notification("error", error.Description);
                    response.AddNotification(notificacao);
                }
            }

            try
            {
                //adicionar usuario na tabela de usuario
                Usuario novoUsuario = new() { IdAspNetUsers = newUser.Id, Nome = request.Nome };
                novoUsuario.IdAspNetUsers = newUser.Id;
                novoUsuario.Nome = request.Nome;
                novoUsuario.Ativo = true;   
                
                await _usuarioRepository.InsertAsync(novoUsuario);

            }
            catch (Exception ex)
            {
                response.Sucesso = false;
                var notificacao = new Notification("Criar usuário", ex.Message);
                response.AddNotification(notificacao);
                return response;
            }

            return response;
        }

        private void Validate(CadastrarUsuarioCommand request)
        {
            if (string.IsNullOrEmpty(request.Password))
            {
                var notification = new Notification("password", "O campo de senha é obrigatório");
                response.AddNotification(notification);
            }

            if (string.IsNullOrEmpty(request.ConfirmPassword))
            {
                var notification = new Notification("password", "O campo de confirmação senha é obrigatório");
                response.AddNotification(notification);
            }

            if (!string.IsNullOrEmpty(request.Password) && !string.IsNullOrEmpty(request.ConfirmPassword) && !request.Password.Equals(request.ConfirmPassword))
            {
                var notification = new Notification("password", "As senhas não conferem");
                response.AddNotification(notification);
            }

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

        public Task<RetornoService> Handle(Erro request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
