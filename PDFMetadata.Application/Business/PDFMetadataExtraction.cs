using Microsoft.Extensions.Logging;
using PDFMetadata.Core.Interfaces;
using PDFMetadata.Shared;
using System;
using System.Collections.Concurrent;

namespace PDFMetadata.Application.Business
{
    public class PDFMetadataExtraction : IPDFMetadataExtraction
    {
        private readonly IPDFExtractionService _pdfExtractService;
        private readonly IReportGenerator _reportGenerator;
        private readonly ILogger<PDFMetadataExtraction> _logger;

        public PDFMetadataExtraction(IPDFExtractionService pdfService, IReportGenerator reportGenerator, ILogger<PDFMetadataExtraction> logger)
        {
            _pdfExtractService = pdfService;
            _reportGenerator = reportGenerator;
            _logger = logger;
        }

        public PDFReportModel ProcessFiles(List<string> filePaths, string outputPath)
        {
            var metadataList = new ConcurrentBag<PDFMetadataModel>();
            var metadataMissingFiles = new ConcurrentBag<string>();
            var processedFileNames = filePaths.Select(path => Path.GetFileName(path)).ToList();
            Parallel.ForEach(filePaths, filePath =>
            {
                try
                {
                    var metadata = _pdfExtractService.ExtractMetadata(filePath);
                    metadataList.Add(metadata);

                    if (string.IsNullOrWhiteSpace(metadata.Title) ||
                        string.IsNullOrWhiteSpace(metadata.Author) ||
                        (metadata.CreationDate == DateTime.MinValue))
                    {
                        metadataMissingFiles.Add(metadata.FileName);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Failed to process file: {filePath}");
                }
            });

            var report = new PDFReportModel
            {
                ProcessedFiles = processedFileNames,
                FilesWithMissingMetadata = metadataMissingFiles.ToList(),
                PDAMetadataList = metadataList,
                TotalPages = metadataList.Sum(m => m.NumberOfPages)
            };

            _reportGenerator.GenerateReport(report, outputPath);
            return report;
        }
    }
}
