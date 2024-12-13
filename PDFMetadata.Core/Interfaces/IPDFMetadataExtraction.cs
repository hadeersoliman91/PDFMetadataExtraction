using PDFMetadata.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDFMetadata.Core.Interfaces
{
    public interface IPDFMetadataExtraction
    {
        PDFReportModel ProcessFiles(List<string> filePaths, string outputPath);
    }
}
