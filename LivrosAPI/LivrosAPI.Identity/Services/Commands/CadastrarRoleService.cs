using Flunt.Notifications;
using LivrosAPI.Application.Models.Identity;
using LivrosAPI.Application.Models.Identity.Commands;
using LivrosAPI.Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace LivrosAPI.Identity.Services.Commands
{
    public class CadastrarRoleService : IRequestHandler<CadastrarRoleCommand, RetornoService>
    {
        private RetornoService response;
        private readonly RoleManager<ApplicationRole> _roleManager;

        public CadastrarRoleService(RoleManager<ApplicationRole> roleManager)
        {
            _roleManager = roleManager;
            response = new RetornoService();
        }
        public async Task<RetornoService> Handle(CadastrarRoleCommand role, CancellationToken cancellationToken)
        {
            IdentityResult result = new IdentityResult();

            var roleExist = await _roleManager.RoleExistsAsync(role.Name);
            if (!roleExist)
            {
                var identityRole = new ApplicationRole();
                identityRole.Name = role.Name;
                result = await _roleManager.CreateAsync(identityRole);
            }

            Notification notificacao;

            if (result.Succeeded)
            {
                response.Sucesso = true;
            }
            else
            {
                response.Sucesso = false;

                var listError = result.Errors;

                foreach (var error in listError)
                {
                    notificacao = new Notification("error", error.Description);
                    response.AddNotification(notificacao);
                }
            }

            return response;
        }
    }
}
