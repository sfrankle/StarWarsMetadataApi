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

    public async Task<JObject> GetSingleRequestAsync(string url)
    {
        var response = await _httpClient.GetAsync(url);
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();
        return JObject.Parse(json);
    }

    public async Task<JObject> GetMultiRequestAsync(string type, int id, IEnumerable<string> attributesToInclude)
    {
        JObject jObject = await GetSingleRequestAsync(type, id);

        var attributes = jObject.Properties().Select(p => p.Name);

        foreach (var attribute in attributesToInclude)
        {
            if (attributes.Contains(attribute))
            {
                var attributePath = jObject.GetValue(attribute).ToString();
                var attributeObject = await GetSingleRequestAsync(attributePath);
                _logger.LogInformation(attributeObject.ToString());
                jObject[attribute].Replace(attributeObject);
            }
            else
            {
                _logger.LogWarning("Attribute requested that was not found: {}", attribute);
            }
        }

        return jObject;
    }
}