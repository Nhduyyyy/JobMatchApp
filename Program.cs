using System;
using System.Net.Http.Headers;
using JobMatchApp.Data;
using JobMatchApp.DBcontext;
using JobMatchApp.Services;
using Microsoft.EntityFrameworkCore;
using IEmbeddingService = JobMatchApp.Services.IEmbeddingService;

var builder = WebApplication.CreateBuilder(args);

// 1) MVC
builder.Services.AddControllersWithViews();

// 2) EF Core
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString)
);

// 3) OpenAI EmbeddingService via HttpClient
builder.Services.AddHttpClient<IEmbeddingService, EmbeddingService>(client =>
{
    client.BaseAddress = new Uri("https://api.openai.com/");
    client.DefaultRequestHeaders.Authorization =
        new AuthenticationHeaderValue("Bearer",
            Environment.GetEnvironmentVariable("OPENAI_API_KEY")!
        );
});

// 4) Các service khác
builder.Services.AddTransient<IResumeExtractor, ResumeExtractor>();    // OCR + Word
builder.Services.AddHttpClient<IResumeParser, ResumeParser>(client =>   // Chat completions via HttpClient
{
    client.BaseAddress = new Uri("https://api.openai.com/");
    client.DefaultRequestHeaders.Authorization =
        new AuthenticationHeaderValue("Bearer",
            Environment.GetEnvironmentVariable("OPENAI_API_KEY")!
        );
});
builder.Services.AddTransient<IJobMatchService, JobMatchService>();

var app = builder.Build();

// 5) Seed dữ liệu Jobs (chạy 1 lần)
using(var scope = app.Services.CreateScope())
{
    var db    = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    var embed = scope.ServiceProvider.GetRequiredService<IEmbeddingService>();
    await JobSeeder.SeedAsync(db, embed);
}

// 6) Middleware pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

// 7) Route mặc định
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Job}/{action=Index}/{id?}"
);

app.Run();
