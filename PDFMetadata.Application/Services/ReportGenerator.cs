using PDFMetadata.Core.Interfaces;
using PDFMetadata.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDFMetadata.Application.Services
{
    public class ReportGenerator : IReportGenerator
    {
        public void GenerateReport(PDFReportModel report, string outputPath)
        {
            var json = System.Text.Json.JsonSerializer.Serialize(report, new System.Text.Json.JsonSerializerOptions
            {
                WriteIndented = true
            });
            File.WriteAllText(outputPath, json);
        }
    }
}
