using System.Text.Json;
using MetadataApi.Utilities;

namespace MetadataApi.Services;

public class StarWarsService : IStarWarsService
{

    private readonly ILogger _logger;
    private readonly HttpClient _httpClient;
    public StarWarsService(ILogger<StarWarsService> logger, HttpClient httpClient)
    {
        _logger = logger;
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<string>> GetAvailableTypesAsync()
    {
        var response = await _httpClient.GetAsync(UrlUtility.GetDomain());
        var json = await response.Content.ReadAsStringAsync();
        var document = JsonDocument.Parse(json);
        return document.RootElement.EnumerateObject().Select(x => x.Name);
    }

    public async Task<JsonDocument> GetSingleRequestAsync(string type, int id)
    {
        var response = await _httpClient.GetAsync(UrlUtility.GetUrl(type, id));
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();
        return JsonDocument.Parse(json);
    }

    public Task<JsonDocument> GetMultiRequestAsync(string path)
    {
        throw new NotImplementedException();
    }


}