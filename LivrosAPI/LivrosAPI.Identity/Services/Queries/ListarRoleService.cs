using Flunt.Notifications;
using LivrosAPI.Application.Contracts.Persistence.Repositories;
using LivrosAPI.Application.Models.Identity;
using LivrosAPI.Application.Models.Identity.Queries;
using LivrosAPI.Application.Responses;
using MediatR;

namespace LivrosAPI.Identity.Services.Queries
{
    public class ListarRoleService : IRequestHandler<ListarRoleQuery, RetornoService>
    {
        private RetornoService response;
        private readonly IRepository<RoleVM> _roleRepository;

        public ListarRoleService(IRepository<RoleVM> roleRepository)
        {
            _roleRepository = roleRepository;
            response = new RetornoService();
        }
        public async Task<RetornoService> Handle(ListarRoleQuery role, CancellationToken cancellationToken)
        {
            try
            {               
                List<RoleVM> list = await _roleRepository.GetAllAsync();
                
                response.AddValue(list);
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
