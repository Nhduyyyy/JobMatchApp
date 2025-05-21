using JobMatchApp.Models;

namespace JobMatchApp.Services;

public interface IJobMatchService
{
    Task<List<JobPosting>> FindBestMatchesAsync(string resumeText, int topN=5);
}
