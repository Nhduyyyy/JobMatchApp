using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace JobMatchApp.Services
{
    public class EmbeddingService : IEmbeddingService
    {
        private readonly HttpClient _http;
        public EmbeddingService(HttpClient http) => _http = http;

        public async Task<float[]> GetEmbeddingAsync(string input)
        {
            // Chuẩn bị payload JSON
            var payload = new {
                model = "text-embedding-ada-002",
                input = new[] { input }
            };
            var content = new StringContent(
                JsonSerializer.Serialize(payload),
                Encoding.UTF8,
                "application/json"
            );

            // Gửi POST /v1/embeddings
            var resp = await _http.PostAsync("v1/embeddings", content);
            resp.EnsureSuccessStatusCode();

            // Đọc kết quả JSON
            using var doc = JsonDocument.Parse(await resp.Content.ReadAsStringAsync());
            var arr = doc.RootElement
                .GetProperty("data")[0]
                .GetProperty("embedding")
                .EnumerateArray();

            // Chuyển thành float[]
            var list = new List<float>();
            foreach (var item in arr)
                list.Add(item.GetSingle());
            return list.ToArray();
        }
    }
}