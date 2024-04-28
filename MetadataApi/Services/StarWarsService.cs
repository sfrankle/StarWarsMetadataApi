using System.Text.Json;

namespace MetadataApi.Services;

public class StarWarsService : IStarWarsService
{

    private readonly ILogger _logger;
    private readonly HttpClient _httpClient;
    private static string domain = "https://swapi.dev/api/";

    public StarWarsService(ILogger<StarWarsService> logger, HttpClient httpClient)
    {
        _logger = logger;
        _httpClient = httpClient;
    }

    public async Task<JsonDocument> GetSingleRequestAsync(string path)
    {
        try
        {
            var response = await _httpClient.GetAsync(domain + path);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var j = JsonDocument.Parse(json);
            return j;
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError("something went wrong");
            _logger.LogError(ex.Message);
            throw new Exception("custom exception");
        }
    }

    public Task<JsonDocument> GetMultiRequestAsync(string path)
    {
        throw new NotImplementedException();
    }
}