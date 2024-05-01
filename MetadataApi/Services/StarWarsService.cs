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
        return await GetSingleRequestAsync(UrlUtility.GetUrl(type, id));
    }

    public async Task<JObject> GetHydratedRequestAsync(string type, int id, HashSet<string> propertiesToReplace)
    {
        JObject jObject = await GetSingleRequestAsync(type, id);

        var allProperties = jObject.Properties().Select(p => p.Name).ToHashSet();

        foreach (var propertyKey in propertiesToReplace)
        {
            if (allProperties.Contains(propertyKey))
            {
                var propertyValue = jObject.GetValue(propertyKey);
                if (propertyValue is null)
                {
                    _logger.LogWarning("Property Value requested was not found: {}", propertyKey);
                    // todo: consider throwing error
                    continue;
                }
                else if (propertyValue is JArray)
                    await ReplaceListObject(jObject, propertyKey, (JArray)propertyValue);
                else
                    await ReplaceSingleObject(jObject, propertyKey, propertyValue.ToString());
            }
            else
                _logger.LogWarning("Property Key requested was not found: {}", propertyKey);
        }

        return jObject;
    }

    private async Task<JObject> GetSingleRequestAsync(string url)
    {
        if (!UrlUtility.Validate(url))
        {
            throw new ArgumentException("Url was not valid");
        }

        var response = await _httpClient.GetAsync(url);
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();
        return JObject.Parse(json);
    }

    private async Task ReplaceSingleObject(JObject jObject, string propertyKey, string attributeUrl)
    {
        var property = await GetSingleRequestAsync(attributeUrl);
        jObject[propertyKey]!.Replace(property);
    }

    private async Task ReplaceListObject(JObject jObject, string propertyKey, JArray propertyValue)
    {
        var hydratedProperties = new JArray();
        foreach (var attributeUrl in propertyValue)
        {
            var property = await GetSingleRequestAsync(attributeUrl.ToString());
            hydratedProperties.Add(property);
        }
        jObject[propertyKey]!.Replace(hydratedProperties);
    }
}