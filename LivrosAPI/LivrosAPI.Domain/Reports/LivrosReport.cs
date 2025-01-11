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
    public class LivrosReport : LivrosReportBase
    {
        public List<LivroDto> _livros;

        public LivrosReport(List<LivroDto> livros)
        {
            _livros = livros;
        }

        public override DataTable GetDadosLivro()
        {
            DataTable dadosLivro = new DataTable();

            dadosLivro.Columns.Add("Id", typeof(int));
            dadosLivro.Columns.Add("Titulo", typeof(string));
            dadosLivro.Columns.Add("Editora", typeof(string));
            dadosLivro.Columns.Add("Edicao", typeof(int));
            dadosLivro.Columns.Add("AnoPublicacao", typeof(string));
            dadosLivro.Columns.Add("DataCriacao", typeof(string));
            dadosLivro.Columns.Add("Assuntos", typeof(string));
            dadosLivro.Columns.Add("Autores", typeof(string));
            dadosLivro.Columns.Add("Valores", typeof(string));

            foreach (var livro in _livros)
            {
                dadosLivro.Rows.Add(
                    livro.Id,
                    livro.Titulo,
                    livro.Editora,
                    livro.Edicao,
                    livro.AnoPublicacao,
                    livro.DataCriacao.ToString("dd-MM-yyyy hh:ss"),
                    GetResumoAssunto(livro.Assuntos),
                    GetResumoAutores(livro.Autores),
                    GetResumoValores(livro.Valores)
                );
            }

            return dadosLivro;
        }

        private string GetResumoAssunto(List<LivroDtoAssunto> assuntos)
        {
            return assuntos is not null && assuntos.Any() ? string.Join("\n", assuntos.Select(x => x.Descricao)) : string.Empty;
        }
        private string GetResumoAutores(List<LivroDtoAutor> autores)
        {
            return autores is not null && autores.Any() ? string.Join("\n", autores.Select(x => x.Nome)) : string.Empty;
        }
        private string GetResumoValores(List<LivroDtoValor> valores)
        {
            return valores is not null && valores.Any() ? string.Join("\n", valores.Select(x => $"{x.DenominacaoFormaCompra}: R$ {x.Valor.ToString("F2", CultureInfo.InvariantCulture).Replace('.', ',')}")) : string.Empty;
        }
    }
}
