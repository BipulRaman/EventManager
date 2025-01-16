using System.Text.Json.Serialization;

namespace EventManager.App.Api.Basic.Models;

public class BaseData
{
    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("createdAt")]
    public DateTimeOffset? CreatedAt { get; set; }

    [JsonPropertyName("modifiedAt")]
    public DateTimeOffset? ModifiedAt { get; set; }

    [JsonPropertyName("createdBy")]
    public string CreatedBy { get; set; }

    [JsonPropertyName("createdByName")]
    public string CreatedByName { get; set; }

    [JsonPropertyName("modifiedBy")]
    public string ModifiedBy { get; set; }
}
