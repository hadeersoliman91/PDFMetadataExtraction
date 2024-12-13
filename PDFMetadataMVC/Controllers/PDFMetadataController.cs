using iText.Layout.Element;
using Microsoft.AspNetCore.Mvc;
using PDFMetadata.Core.Interfaces;
using PDFMetadata.Shared;
using System.Collections.Generic;

namespace PDFMetadataMVC.Controllers
{
    public class PDFMetadataController : Controller
    {

        private readonly IPDFExtractionService _pdfMetadataService;

        private readonly IPDFMetadataExtraction _pdfMetadataExtraction;

        public PDFMetadataController(IPDFExtractionService pdfMetadataService, IPDFMetadataExtraction pdfMetadataExtraction)
        {
            _pdfMetadataService = pdfMetadataService;

            _pdfMetadataExtraction = pdfMetadataExtraction;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Upload(List<IFormFile> files)
        {
            var allFilePaths = new List<string>();
            var reportPDF = new PDFReportModel();
            var dir = Path.Combine(Directory.GetCurrentDirectory(), "UploadedFiles");
            if (files != null && files.Count > 0)
            {
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }

                foreach (var file in files)
                {
                    var filePath = Path.Combine(dir, file.FileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }
                    allFilePaths.Add(filePath);

                }
            }

            if (allFilePaths.Count > 0)
            {
                reportPDF = _pdfMetadataExtraction.ProcessFiles(allFilePaths, "wwwroot/PDFMetadata.json");
            }

            return View("DisplayMetadata", reportPDF);
        }
    }
}
