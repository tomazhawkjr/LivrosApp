using LivrosAPI.Application.Responses;
using MediatR;

namespace LivrosAPI.Application.Features.FormaCompra.Commands.DeletarFormaCompra
{
    public class DeletarFormaCompraCommand : IRequest<RetornoService>
    {
        public int Id { get; set; }
    }
}