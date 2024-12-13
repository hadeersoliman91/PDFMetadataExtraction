using PDFMetadata.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDFMetadata.Core.Interfaces
{
    public interface IPDFExtractionService
    {
        PDFMetadataModel ExtractMetadata(string filePath);
    }
}
