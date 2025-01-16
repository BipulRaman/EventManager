using System.Text.Json.Serialization;

namespace EventManager.App.Api.Extended.Models;

public class ProfileDataUpdate : ProfileData
{
    [JsonIgnore]
    public new string Email { get; set; } = string.Empty;

    [JsonIgnore]
    public new string PrimarySchoolName { get; set; } = string.Empty;

    [JsonIgnore]
    public new string SecondarySchoolNames { get; set; } = string.Empty;

    [JsonIgnore]
    public new List<string> Roles { get; set; } = new List<string>();

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
