using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using JobMatchApp.Models;

namespace JobMatchApp.Services
{
    public class ResumeParser : IResumeParser
    {
        private readonly HttpClient _http;
        public ResumeParser(HttpClient http) => _http = http;

        public async Task<ResumeProfile> ParseAsync(string rawText)
        {
            // 1) Chuẩn bị payload gửi lên Chat API
            var payload = new
            {
                model = "gpt-3.5-turbo",
                messages = new[]
                {
                    new { role = "system",
                          content = "Bạn là AI parse CV thành JSON với các trường: " +
                                    "FullName, Email, Phone, Skills[], Experiences[]; " +
                                    "Trả đúng JSON, không kèm chú thích." },
                    new { role = "user", content = rawText }
                }
            };
            var content = new StringContent(
                JsonSerializer.Serialize(payload),
                Encoding.UTF8,
                "application/json"
            );

            // 2) Gọi endpoint /v1/chat/completions
            var resp = await _http.PostAsync("v1/chat/completions", content);
            resp.EnsureSuccessStatusCode();

            // 3) Đọc phần message.content chứa JSON
            using var doc = JsonDocument.Parse(await resp.Content.ReadAsStringAsync());
            var msg = doc.RootElement
                         .GetProperty("choices")[0]
                         .GetProperty("message")
                         .GetProperty("content")
                         .GetString()!;

            // 4) Parse thủ công JSON thành ResumeProfile
            using var json = JsonDocument.Parse(msg);
            var root = json.RootElement;

            var profile = new ResumeProfile
            {
                FullName    = root.GetProperty("FullName").GetString() ?? "",
                Email       = root.GetProperty("Email").GetString() ?? "",
                Phone       = root.GetProperty("Phone").GetString() ?? "",

                Skills = root.GetProperty("Skills")
                             .EnumerateArray()
                             .Select(e => e.GetString() ?? "")
                             .ToArray(),

                Experiences = root.GetProperty("Experiences")
                                 .EnumerateArray()
                                 .Select(e => e.ValueKind switch
                                 {
                                     JsonValueKind.String => e.GetString()!,
                                     JsonValueKind.Object =>
                                         // Nếu object, convert nguyên object thành string JSON
                                         e.GetRawText(),
                                     _ =>
                                         // Các ValueKind khác, fallback ToString()
                                         e.ToString()!
                                 })
                                 .ToArray()
            };

            return profile;
        }
    }
}
