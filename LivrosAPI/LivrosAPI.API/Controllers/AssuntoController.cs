using LivrosAPI.Infrastructure;
using LivrosAPI.Infrastructure.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using LivrosAPI.Application.Features.Assunto.Commands.CadastrarAssunto;
using LivrosAPI.Application.Features.Assunto.Commands.AtualizarAssunto;
using LivrosAPI.Application.Features.Assunto.Commands.DeletarAssunto;
using Microsoft.AspNetCore.Authorization;

namespace LivrosAPI.API.Controllers
{
  [Authorize(AuthenticationSchemes = "Bearer")]
  [ApiController]
  [Route("api/[controller]")]
  public class AssuntoController : BaseApiController
  {

    public AssuntoController(IMediator mediator) : base(mediator)
    {
    }

    [HttpPost]
    public async Task<ServiceHttpResult> CadastrarAssunto([FromBody] CadastrarAssuntoCommand model)
    {
      return await HandleRequest(model, "Assunto cadastrado com sucesso!");
    }

    [HttpGet]
    public async Task<ServiceHttpResult> BuscarAssunto()
    {
      BuscarAssuntoQuery model = new BuscarAssuntoQuery();
      return await HandleRequest(model);
    }

    [HttpPut]
    public async Task<ServiceHttpResult> AtualizarAssunto([FromBody] AtualizarAssuntoCommand model)
    {
      return await HandleRequest(model, "Assunto alterada com sucesso!");
    }

    [HttpDelete]
    public async Task<ServiceHttpResult> DeletarAssunto([FromBody] DeletarAssuntoCommand model)
    {
      return await HandleRequest(model, "Item deletado com sucesso da Assunto!");
    }
  }
}