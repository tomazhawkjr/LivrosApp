using LivrosAPI.Domain.Entities;
using LivrosAPI.Domain.Models.Dtos;
using LivrosAPI.Domain.Reports.Base;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LivrosAPI.Domain.Reports
{
    public class LivrosByAutorReport : LivrosReportBase
    {
        public List<LivroByAutorDto> _livros;

        public LivrosByAutorReport(List<LivroByAutorDto> livros)
        {
            _livros = livros;
        }

        public override DataTable GetDadosLivro()
        {
            DataTable dadosLivro = new DataTable();

            dadosLivro.Columns.Add("Id", typeof(int));
            dadosLivro.Columns.Add("Nome", typeof(string));
            dadosLivro.Columns.Add("Livros", typeof(string));

            foreach (var autor in _livros)
            {
                dadosLivro.Rows.Add(
                    autor.Id,
                    autor.Nome,
                    GetResumoLivros(autor.Livros)                    
                );
            }

            return dadosLivro;
        }

        private string GetResumoLivros(List<LivroDtoByAutorLivro> livros)
        {
            StringBuilder sb = new StringBuilder();
            string separador = "; ";
            
            foreach (var livro in livros)
            {
                sb.AppendLine(livro.Titulo);
                sb.Append($"Editora: {livro.Editora}")
                   .Append(separador)
                   .Append($"Edição: {livro.Edicao}")
                   .Append(separador)
                   .AppendLine($"Ano Publicação: {livro.AnoPublicacao}");
                sb.AppendLine($"Assuntos: {GetResumoAssunto(livro.Assuntos)}");
                sb.AppendLine(GetResumoValores(livro.Valores));
                sb.AppendLine(string.Empty);
            }

            return sb.ToString();
        }
        private string GetResumoAssunto(List<LivroDtoAssunto> assuntos)
        {
            return assuntos is not null && assuntos.Any() ? string.Join(", ", assuntos.Select(x => x.Descricao)) : string.Empty;
        }
        private string GetResumoValores(List<LivroDtoValor> valores)
        {
            return valores is not null && valores.Any() ? string.Join(", ", valores.Select(x => $"{x.DenominacaoFormaCompra}: R$ {x.Valor.ToString("F2", CultureInfo.InvariantCulture).Replace('.', ',')}")) : string.Empty;
        }
    }
}
