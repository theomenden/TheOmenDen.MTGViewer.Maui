using System.Text.Json.Serialization;

namespace MTGViewer.Maui.Data.MtgJson;
public sealed class MtgJsonConfiguration
{
    [JsonPropertyName("graphQL")]
    public string GraphQLEndpoint { get; set; } = String.Empty;
    [JsonPropertyName("graphKey")]
    public string GraphQLKey { get; set; } = String.Empty;
}
