using JobMatchApp.Models;

namespace JobMatchApp.Services;

public interface IResumeParser
{
    Task<ResumeProfile> ParseAsync(string rawText);
}
