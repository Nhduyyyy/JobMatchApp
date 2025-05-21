using DocumentFormat.OpenXml.Office2013.Word;
using JobMatchApp.Models;
using Microsoft.EntityFrameworkCore;

namespace JobMatchApp.DBcontext;

public class ApplicationDbContext : Microsoft.EntityFrameworkCore.DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    { }

    public DbSet<JobPosting> JobPostings { get; set; }
}