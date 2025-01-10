using LivrosAPI.Application.Features.Livro.Commands.DeletarLivro;
using LivrosAPI.Application.Features.Livro.Commands.CadastrarLivro;
using LivrosAPI.Infrastructure;
using LivrosAPI.Infrastructure.Services;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using LivrosAPI.Application.Features.Livro.Commands.AtualizarLivro;

namespace LivrosAPI.API.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [ApiController]
    [Route("api/[controller]")]
    public class LivroController : BaseApiController
    {

        public LivroController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost]
        public async Task<ServiceHttpResult> CadastrarLivro([FromBody] CadastrarLivroCommand model)
        {
            return await HandleRequest(model, "Livro cadastrado com sucesso!");
        }

        [HttpGet]
        public async Task<ServiceHttpResult> BuscarLivro()
        {
            BuscarLivroQuery model = new BuscarLivroQuery();
            return await HandleRequest(model);
        }

        [HttpGet("EmitirRelatorio")]
        public async Task<IActionResult> EmitirRelatorio()
        {
            RelatorioLivroQuery model = new RelatorioLivroQuery();
            var result = await HandleRequest(model);

            byte[] dados = result.ServiceResponse.DataFile;

            return File(dados, "application/pdf", "Relatorio.pdf");
        }

        [HttpPut]
        public async Task<ServiceHttpResult> AtualizarLivro([FromBody] AtualizarLivroCommand model)
        {
            return await HandleRequest(model, "Livro alterada com sucesso!");
        }

        [HttpDelete]
        public async Task<ServiceHttpResult> DeletarLivro([FromBody] DeletarLivroCommand model)
        {
            return await HandleRequest(model, "Item deletado com sucesso da Livro!");
        }
    }
}