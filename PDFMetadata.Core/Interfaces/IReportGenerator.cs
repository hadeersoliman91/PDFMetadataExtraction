
using PDFMetadata.Shared;

namespace PDFMetadata.Core.Interfaces
{
    public interface IReportGenerator
    {
        void GenerateReport(PDFReportModel report, string outputPath);
    }
}
