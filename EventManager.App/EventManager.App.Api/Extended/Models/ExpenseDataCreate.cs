using System.Text.Json.Serialization;

namespace EventManager.App.Api.Extended.Models;

public class ExpenseDataCreate : ExpenseData
{
    [JsonIgnore]
    public new string Id { get; set; } = string.Empty;

    [JsonIgnore]
    public new string CreatedBy { get; set; } = string.Empty;

    [JsonIgnore]
    public new string CreatedByName { get; set; } = string.Empty;

    [JsonIgnore]
    public new DateTimeOffset? CreatedAt { get; set; }

    [JsonIgnore]
    public new DateTimeOffset? ModifiedAt { get; set; }

    [JsonIgnore]
    public new string ModifiedBy { get; set; } = string.Empty;
}
