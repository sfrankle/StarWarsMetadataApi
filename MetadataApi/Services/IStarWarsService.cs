using System.Text.Json;

namespace MetadataApi.Services;

public interface IStarWarsService
{
    Task<IEnumerable<string>> GetAvailableTypesAsync();
    Task<JsonDocument> GetSingleRequestAsync(string type, int id);
    Task<JsonDocument> GetMultiRequestAsync(string path);
}
