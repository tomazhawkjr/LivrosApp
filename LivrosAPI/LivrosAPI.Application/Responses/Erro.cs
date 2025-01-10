using LivrosAPI.Domain.Entities.Base;
using MediatR;
using System.ComponentModel.DataAnnotations.Schema;

namespace LivrosAPI.Application.Responses
{
    [Table("Erro")]
    public class Erro : Entidade, IRequest<RetornoService>
    {
        public string? Aplicacao { get; set; }
        public DateTime DataHora { get; set; }
        public string? StackTrace { get; set; }
        public string? InformacaoAdicional { get; set; }
        public string? Mensagem { get; set; }

    }
}
