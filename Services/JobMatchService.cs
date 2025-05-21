using JobMatchApp.DBcontext;
using JobMatchApp.Models;
using Microsoft.EntityFrameworkCore;

namespace JobMatchApp.Services;

public class JobMatchService : IJobMatchService
{
    private readonly ApplicationDbContext _db;
    private readonly IEmbeddingService _embed;
    public JobMatchService(ApplicationDbContext db, IEmbeddingService embed)
    { _db=db; _embed=embed; }

    public async Task<List<JobPosting>> FindBestMatchesAsync(string resumeText, int topN=5)
    {
        var rv  = await _embed.GetEmbeddingAsync(resumeText);
        var all = await _db.JobPostings.ToListAsync();
        return all
            .Select(j => new {
                Job   = j,
                Score = Cosine(rv,j.Embedding)
            })
            .OrderByDescending(x=>x.Score)
            .Take(topN)
            .Select(x=>x.Job)
            .ToList();
    }

    float Cosine(float[] a, float[] b)
    {
        float dot=0,ma=0,mb=0;
        for(int i=0;i<a.Length;i++){
            dot+=a[i]*b[i];
            ma+=a[i]*a[i];
            mb+=b[i]*b[i];
        }
        return dot/(MathF.Sqrt(ma)*MathF.Sqrt(mb));
    }
}
