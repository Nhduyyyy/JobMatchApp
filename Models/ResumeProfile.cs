namespace JobMatchApp.Models;

public class ResumeProfile
{
    public string FullName { get; set; }
    public string Email    { get; set; }
    public string Phone    { get; set; }
    public string[] Skills { get; set; }
    public string[] Experiences { get; set; }
}
