using LivrosAPI.Application.Models.Identity;
using LivrosAPI.Application.Models.Identity.Commands;
using LivrosAPI.Application.Models.Identity.Queries;
using LivrosAPI.Infrastructure;
using LivrosAPI.Infrastructure.Services;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LivrosAPI.API.Controllers
{
  [Authorize(AuthenticationSchemes = "Bearer")]
  [ApiController]
  [Route("api/[controller]")]
  public class AccountController : BaseApiController
  {

    public AccountController(IMediator mediator) : base(mediator)
    {
    
    }

    /// <summary>
    /// Cadastrar um novo usuário
    /// </summary>
    /// <param name="command">Informações para criar um usuário</param>
    /// <returns>Um servço de retorno</returns>
    [HttpPost]
    [AllowAnonymous]
    public async Task<ServiceHttpResult> CadastrarUsuario([FromBody] CadastrarUsuarioCommand model)
    {
      return await HandleRequest(model);
    }

    /// <summary>
    /// Autentica um usuário na api
    /// </summary>
    /// <param name="command">Informações de login</param>
    /// <returns>TSuccessen JWT</returns>
    [AllowAnonymous]
    [HttpPost("Login")]
    public async Task<ServiceHttpResult> Login([FromBody] LoginCommand autenticacao)
    {
      return await HandleRequest(autenticacao);
    }

    /// <summary>
    /// Alterar a senha do usuário
    /// </summary>
    /// <returns>Um servço de retorno</returns>
    [AllowAnonymous]
    [HttpPost("AlterarSenha")]
    public async Task<ServiceHttpResult> AlterarSenha([FromBody] AlterarSenhaCommand model)
    {
      return await HandleRequest(model, "Senha alterada com sucesso");
    }
    
    //POST api/Usuario/CriarRole
    // <summary>
    //  Cria uma role
    // </summary>
    [HttpPost("Role")]
    private async Task<ServiceHttpResult> CadastrarRole([FromBody] CadastrarRoleCommand role)
    {
      return await HandleRequest(role);
    }   

    /// <summary>
    ///  lista as roles
    /// </summary>
    [HttpGet("Role")]
    public async Task<ServiceHttpResult> ListarRole()
    {

      ListarRoleQuery model = new ListarRoleQuery();
      return await HandleRequest(model);

    }

    /// <summary>
    /// Atualizar usuário 
    /// </summary>
    /// <returns>Um serviço de retorno</returns>
    [HttpPut]
    public async Task<ServiceHttpResult> AtualizarUsuario([FromBody] AtualizarUsuarioCommand model)
    {
      return await HandleRequest(model, "O usuário foi atualizado com sucesso");
    }

    /// <returns>Um serviço de retorno</returns>
    [HttpDelete]
    public async Task<ServiceHttpResult> DeletarUsuario([FromQuery] DeletarUsuarioCommand model)
    {
      var response = await _mediator.Send(model);

      if (!response.Sucesso)
      {
        return Error(response.GetListaMensagemToString());
      }

      return Success("O usuário foi deletado com sucesso");
    }
  }
}