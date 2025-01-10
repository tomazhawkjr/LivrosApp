using LivrosAPI.Infrastructure;
using LivrosAPI.Infrastructure.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using LivrosAPI.Application.Features.FormaCompra.Commands.CadastrarFormaCompra;
using LivrosAPI.Application.Features.FormaCompra.Commands.AtualizarFormaCompra;
using LivrosAPI.Application.Features.FormaCompra.Commands.DeletarFormaCompra;
using Microsoft.AspNetCore.Authorization;

namespace LivrosAPI.API.Controllers
{
  [Authorize(AuthenticationSchemes = "Bearer")]
  [ApiController]
  [Route("api/[controller]")]
  public class FormaCompraController : BaseApiController
  {

    public FormaCompraController(IMediator mediator) : base(mediator)
    {
    }

    [HttpPost]
    public async Task<ServiceHttpResult> CadastrarFormaCompra([FromBody] CadastrarFormaCompraCommand model)
    {
      return await HandleRequest(model, "FormaCompra cadastrado com sucesso!");
    }

    [HttpGet]
    public async Task<ServiceHttpResult> BuscarFormaCompra()
    {
      BuscarFormaCompraQuery model = new BuscarFormaCompraQuery();
      return await HandleRequest(model);
    }

    [HttpPut]
    public async Task<ServiceHttpResult> AtualizarFormaCompra([FromBody] AtualizarFormaCompraCommand model)
    {
      return await HandleRequest(model, "FormaCompra alterada com sucesso!");
    }

    [HttpDelete]
    public async Task<ServiceHttpResult> DeletarFormaCompra([FromBody] DeletarFormaCompraCommand model)
    {
      return await HandleRequest(model, "Item deletado com sucesso da FormaCompra!");
    }
  }
}