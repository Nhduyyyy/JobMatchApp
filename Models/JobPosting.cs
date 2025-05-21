namespace JobMatchApp.Models;

public class JobPosting
{
    public int Id { get; set; }
    public string Title { get; set; } = "";
    public string Description { get; set; } = "";
    public string[] RequiredSkills { get; set; } = Array.Empty<string>();
    public float[] Embedding { get; set; } = Array.Empty<float>();
}
