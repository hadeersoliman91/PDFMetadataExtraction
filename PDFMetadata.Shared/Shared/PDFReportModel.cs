
namespace PDFMetadata.Shared
{
    public class PDFReportModel
    {
        public List<string> ProcessedFiles { get; set; } = new();
        public List<string> FilesWithMissingMetadata { get; set; } = new();
        public IEnumerable<PDFMetadataModel> PDAMetadataList { get; set; }
        public int TotalPages { get; set; }
    }
}
