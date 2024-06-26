namespace MetadataApi.Utilities;

public static class UrlUtility
{
    private static string domain = "https://swapi.dev/api/";
    public static string GetDomain() => domain;
    public static string GetUrl(string type, int id)
    {
        return new Uri(new Uri(domain), $"{type}/{id}").ToString();
    }

    public static bool Validate(string url)
    {
        return Uri.IsWellFormedUriString(url, UriKind.Absolute) && url.StartsWith(domain);
    }
}
