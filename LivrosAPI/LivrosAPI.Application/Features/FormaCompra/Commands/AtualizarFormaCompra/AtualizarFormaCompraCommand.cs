using LivrosAPI.Application.Responses;
using MediatR;

namespace LivrosAPI.Application.Features.FormaCompra.Commands.AtualizarFormaCompra
{
    public class AtualizarFormaCompraCommand : Domain.Entities.FormaCompra, IRequest<RetornoService>
    {
    }
}