using LivrosAPI.Application.Responses;
using MediatR;

namespace LivrosAPI.Application.Features.FormaCompra.Commands.CadastrarFormaCompra
{
    public class CadastrarFormaCompraCommand : Domain.Entities.FormaCompra, IRequest<RetornoService>
    {
    }
}

