using System.Text.Json;

namespace MetadataApi.Services;

public interface IStarWarsService
{
    Task<JsonDocument> GetSingleRequestAsync(string path);
    Task<JsonDocument> GetMultiRequestAsync(string path);
}
