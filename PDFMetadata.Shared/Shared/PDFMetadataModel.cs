

namespace PDFMetadata.Shared
{
    public class PDFMetadataModel
    {
        public string FilePath { get; set; } = string.Empty;
        public string FileName { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public DateTime CreationDate { get; set; } 
        public int NumberOfPages { get; set; }
    }
}
