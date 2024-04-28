using MetadataApi.Utilities;
using Newtonsoft.Json.Linq;

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
        var jObject = JObject.Parse(json);
        return jObject.Properties().Select(p => p.Name);
    }

    public async Task<JObject> GetSingleRequestAsync(string type, int id)
    {
        var response = await _httpClient.GetAsync(UrlUtility.GetUrl(type, id));
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();
        return JObject.Parse(json);
    }

    public Task<JObject> GetMultiRequestAsync(string path)
    {
        throw new NotImplementedException();
    }
}