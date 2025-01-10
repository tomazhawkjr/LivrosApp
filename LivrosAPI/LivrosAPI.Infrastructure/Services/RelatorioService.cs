using iText.Kernel.Pdf;
using LivrosAPI.Application.Contracts.Infrastructure.Services;
using LivrosAPI.Domain.Reports;
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

        public byte[] GerarRelatorioLivrosPdf(LivrosReport livroReport)
        {
            if (livroReport == null || livroReport._livros == null || !livroReport._livros.Any())
                return null;

            string reportPath = Path.Combine(Directory.GetCurrentDirectory(), "Livros.rdlc");

            LocalReport report = new LocalReport
            {
                ReportPath = reportPath
            };

            report.DataSources.Add(new ReportDataSource("LivroDataSet", livroReport.GetDadosLivro()));

            byte[] pdfBytes = report.Render(
                format: "PDF"
            );

            report.Dispose();

            return RemoverPaginasPares(pdfBytes);
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
