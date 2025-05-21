namespace JobMatchApp.Services;

public interface IEmbeddingService
{
    Task<float[]> GetEmbeddingAsync(string input);
}
