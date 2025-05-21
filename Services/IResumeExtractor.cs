namespace JobMatchApp.Services;

public interface IResumeExtractor
{
    string ExtractTextFromImage(string path);
    string ExtractTextFromDocx(string path);
}
