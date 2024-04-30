using Newtonsoft.Json.Linq;

namespace MetadataApi.Services;

public interface IStarWarsService
{
    Task<IEnumerable<string>> GetAvailableTypesAsync();
    Task<JObject> GetSingleRequestAsync(string type, int id);
    Task<JObject> GetHydratedRequestAsync(string type, int id, HashSet<string> attributesToInclude);
}
