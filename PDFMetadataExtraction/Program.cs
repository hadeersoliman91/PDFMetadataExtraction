// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PDFMetadata.Application.Business;
using PDFMetadata.Application.Services;
using PDFMetadata.Core.Interfaces;

class Program
{
    static void Main(string[] args)
    {
        // Setup DI
        var serviceProvider = new ServiceCollection()
            .AddLogging(config => config.AddConsole())
            .AddSingleton<IPDFExtractionService, PDFExtractionService>()
            .AddSingleton<IReportGenerator, ReportGenerator>()
            .AddSingleton<PDFMetadataExtraction>()
            .BuildServiceProvider();

        var logger = serviceProvider.GetService<ILogger<Program>>();
        var extraction = serviceProvider.GetService<PDFMetadataExtraction>();

        
        var filePaths = new List<string> { "D://Test//test1.pdf", "D://Test//testAhmed6.pdf" }; 
        var outputPath = "D://Test//pdf_Report.json";

        try
        {
            extraction.ProcessFiles(filePaths, outputPath);
            logger.LogInformation("Processing completed. Report generated at {OutputPath}", outputPath);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred during processing.");
        }
    }
}