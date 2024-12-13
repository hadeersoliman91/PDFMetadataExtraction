using iText.Kernel.Pdf;
using PDFMetadata.Core.Interfaces;
using PDFMetadata.Shared;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDFMetadata.Application.Services
{
    public class PDFExtractionService : IPDFExtractionService
    {
        public PDFMetadataModel ExtractMetadata(string filePath)
        {
            try
            {
                using var pdfReader = new PdfReader(filePath);
                using var pdfDocument = new PdfDocument(pdfReader);
                var info = pdfDocument.GetDocumentInfo();

                return new PDFMetadataModel
                {
                    FilePath = filePath,
                    FileName = Path.GetFileName(filePath),
                    Title = info.GetTitle() ?? string.Empty,
                    Author = info.GetAuthor() ?? string.Empty,
                    CreationDate = DateTime.ParseExact(info.GetMoreInfo("CreationDate").Substring(2, 14), "yyyyMMddHHmmss", CultureInfo.InvariantCulture),
                    NumberOfPages = pdfDocument.GetNumberOfPages()
                };
            }
            catch (Exception ex)
            {
              
                throw new Exception($"Error processing file {filePath}: {ex.Message}", ex);
            }
        }
    }
}
