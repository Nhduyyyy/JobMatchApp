using DocumentFormat.OpenXml.Packaging;
using Tesseract;

namespace JobMatchApp.Services;

public class ResumeExtractor : IResumeExtractor
{
    public string ExtractTextFromImage(string path)
    {
        using var engine = new TesseractEngine(@"./tessdata","eng",EngineMode.Default);
        using var img    = Pix.LoadFromFile(path);
        using var page   = engine.Process(img);
        return page.GetText();
    }
    public string ExtractTextFromDocx(string path)
    {
        using var doc = WordprocessingDocument.Open(path,false);
        return doc.MainDocumentPart!.Document.Body.InnerText;
    }
}
