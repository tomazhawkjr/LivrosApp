using LivrosAPI.Infrastructure;
using LivrosAPI.Infrastructure.Services;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using LivrosAPI.Application.Features.Autor.Commands.CadastrarAutor;
using LivrosAPI.Application.Features.Autor.Commands.AtualizarAutor;
using LivrosAPI.Application.Features.Autor.Commands.DeletarAutor;

namespace LivrosAPI.API.Controllers
{
  [Authorize(AuthenticationSchemes = "Bearer")]
  [ApiController]
  [Route("api/[controller]")]
  public class AutorController : BaseApiController
  {

    public AutorController(IMediator mediator) : base(mediator)
    {
    }

    [HttpPost]
    public async Task<ServiceHttpResult> CadastrarAutor([FromBody] CadastrarAutorCommand model)
    {
      return await HandleRequest(model, "Autor cadastrado com sucesso!");
    }

    [HttpGet]
    public async Task<ServiceHttpResult> BuscarAutor()
    {
      BuscarAutorQuery model = new BuscarAutorQuery();
      return await HandleRequest(model);
    }

    [HttpPut]
    public async Task<ServiceHttpResult> AtualizarAutor([FromBody] AtualizarAutorCommand model)
    {
      return await HandleRequest(model, "Autor alterada com sucesso!");
    }

    [HttpDelete]
    public async Task<ServiceHttpResult> DeletarAutor([FromBody] DeletarAutorCommand model)
    {
      return await HandleRequest(model, "Item deletado com sucesso da Autor!");
    }
  }
}