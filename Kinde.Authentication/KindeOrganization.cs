using System.Text.Json.Serialization;

namespace KindeAuthentication;

public class KindeOrganization
{
    [JsonPropertyName("id")] public string Id { get; set; }
    [JsonPropertyName("name")] public string Name { get; set; }
}