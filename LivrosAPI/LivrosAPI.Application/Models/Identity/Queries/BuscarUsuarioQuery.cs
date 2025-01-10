using LivrosAPI.Application.Responses;
using MediatR;

namespace LivrosAPI.Application.Models.Identity.Queries
{
    public class BuscarUsuarioQuery : IRequest<RetornoService>
    {
        public string? Pesquisa { get; set; }
    }
}
