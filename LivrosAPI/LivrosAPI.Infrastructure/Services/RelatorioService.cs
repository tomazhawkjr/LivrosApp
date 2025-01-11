using iText.Kernel.Pdf;
using LivrosAPI.Application.Contracts.Infrastructure.Services;
using LivrosAPI.Domain.Reports;
using LivrosAPI.Domain.Reports.Base;
using Microsoft.Reporting.NETCore;
using System.Data;
using System.Reflection.PortableExecutable;

namespace LivrosAPI.Infrastructure.Services
{
    public class RelatorioService : IRelatorioService
    {

        public RelatorioService()
        {
        }

        public byte[] GerarRelatorioLivrosByAutorPdf(LivrosByAutorReport livroReport)
        {
            return GerarRelatorioPdf(livroReport, livroReport._livros, "LivrosByAutor.rdlc", "LivroDataSet");
        }

        public byte[] GerarRelatorioLivrosPdf(LivrosReport livroReport)
        {
            return GerarRelatorioPdf(livroReport, livroReport._livros, "Livros.rdlc", "LivroDataSet");
        }

        private byte[] GerarRelatorioPdf<T>(LivrosReportBase livroReport, List<T> _livros, string nomeRdlc, string nomeDataSet, bool removerPaginasPares = true)
        {
            if (livroReport == null || _livros == null || !_livros.Any())
                return null;

            string reportPath = Path.Combine(Directory.GetCurrentDirectory(), nomeRdlc);

            LocalReport report = new LocalReport
            {
                ReportPath = reportPath
            };

            report.DataSources.Add(new ReportDataSource(nomeDataSet, livroReport.GetDadosLivro()));

            byte[] pdfBytes = report.Render(
                format: "PDF"
            );

            report.Dispose();

            if(removerPaginasPares)
                return RemoverPaginasPares(pdfBytes);

            return pdfBytes;
        }

        private byte[] RemoverPaginasPares(byte[] pdfBytes)
        {
            using (MemoryStream inputMemoryStream = new MemoryStream(pdfBytes))
            {
                using (MemoryStream outputMemoryStream = new MemoryStream())
                {                   
                    PdfDocument pdfDocument = new PdfDocument(new PdfReader(inputMemoryStream));
               
                    PdfDocument outputPdf = new PdfDocument(new PdfWriter(outputMemoryStream));
                    
                    int totalPages = pdfDocument.GetNumberOfPages();
                    for (int i = 1; i <= totalPages; i++)
                    {
                        if (i % 2 != 0)
                        {
                            pdfDocument.CopyPagesTo(i, i, outputPdf);
                        }
                    }
                    
                    pdfDocument.Close();
                    outputPdf.Close();

                    return outputMemoryStream.ToArray();
                }
            }
        }
    }
}
