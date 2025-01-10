using Flunt.Notifications;
using LivrosAPI.Application.Models.Identity;
using LivrosAPI.Application.Models.Identity.Commands;
using LivrosAPI.Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace LivrosAPI.Identity.Services.Commands
{
    public class AlterarSenhaService : IRequestHandler<AlterarSenhaCommand, RetornoService>
    {
        private RetornoService response;
        private readonly UserManager<ApplicationUser> _userManager;

        public AlterarSenhaService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            response = new RetornoService();
        }

        public async Task<RetornoService> Handle(AlterarSenhaCommand request, CancellationToken cancellationToken)
        {
            Validate(request);

            if (response.TemMensagens)
            {
                response.Sucesso = false;
                return response;
            }

            var usuario = await _userManager.FindByNameAsync(request.Email);

            string resetToken = await _userManager.GeneratePasswordResetTokenAsync(usuario);

            var result = await _userManager.ResetPasswordAsync(usuario, resetToken, request.Password);

            response.Sucesso = true;

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

            return response;
        }

        private void Validate(AlterarSenhaCommand request)
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
                    notification = new Notification("email", "O campo de email é obrigatório");
                    response.AddNotification(notification);
                }
            }

        }
    }
}
