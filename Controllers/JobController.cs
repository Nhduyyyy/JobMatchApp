using JobMatchApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace JobMatchApp.Controllers;

public class JobController : Controller
{
    private readonly IResumeExtractor _ext;
    private readonly IResumeParser    _parser;
    private readonly IJobMatchService _matcher;

    public JobController(IResumeExtractor ext,
        IResumeParser parser,
        IJobMatchService matcher)
    {
        _ext     = ext;
        _parser  = parser;
        _matcher = matcher;
    }

    [HttpGet]
    public IActionResult Index() => View();

    [HttpPost]
    public async Task<IActionResult> UploadCv(IFormFile file)
    {
        var uploads= Path.Combine(Directory.GetCurrentDirectory(),"wwwroot","uploads");
        Directory.CreateDirectory(uploads);
        var fn  = Guid.NewGuid()+Path.GetExtension(file.FileName);
        var p   = Path.Combine(uploads,fn);
        using(var fs=System.IO.File.Create(p))
            await file.CopyToAsync(fs);

        string raw = Path.GetExtension(p).ToLower()==".docx"
            ? _ext.ExtractTextFromDocx(p)
            : _ext.ExtractTextFromImage(p);

        var profile = await _parser.ParseAsync(raw);
        var matches = await _matcher.FindBestMatchesAsync(raw);
        return View("Results",matches);
    }
}
