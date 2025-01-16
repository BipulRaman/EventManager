using EventManager.App.Api.Basic.Constants;
using EventManager.App.Api.Basic.Models;
using System.Text.Json.Serialization;

namespace EventManager.App.Api.Extended.Models;

public class EventData : BaseData
{
    [JsonPropertyName("title")]
    public string Title { get; set; }

    [JsonPropertyName("details")]
    public string Details { get; set; }

    [JsonPropertyName("startDateTime")]
    public DateTimeOffset StartDateTime { get; set; }

    [JsonPropertyName("endDateTime")]
    public DateTimeOffset EndDateTime { get; set; }

    [JsonPropertyName("location")]
    public string Location { get; set; }

    [JsonPropertyName("link")]
    public string Link { get; set; }

    public bool IsValidToCreate()
    {
        return !string.IsNullOrWhiteSpace(Title)
            && !string.IsNullOrWhiteSpace(Details)
            && !string.IsNullOrWhiteSpace(Location);
    }

    public bool IsValidToUpdate()
    {
        return !string.IsNullOrWhiteSpace(Id)
            && !string.IsNullOrWhiteSpace(Title)
            && !string.IsNullOrWhiteSpace(Details)
            && !string.IsNullOrWhiteSpace(Location);
    }

    public EventEntity ConvertToCreateEntity(HttpContext httpContext)
    {
        User contextUserData = httpContext.Items[NameConstants.USER_KEY] as User;

        return new EventEntity
        {
            PartitionKey = contextUserData.Id,
            RowKey = Guid.NewGuid().ToString(),
            Title = Title,
            Details = Details,
            StartDateTime = StartDateTime,
            EndDateTime = EndDateTime,
            Location = Location,
            Link = Link,
            CreatedAt = new DateTimeOffset(DateTime.UtcNow),
            Timestamp = new DateTimeOffset(DateTime.UtcNow),
            CreatedBy = contextUserData.Id,
            CreatedByName = contextUserData.Name,
            ModifiedBy = contextUserData.Id
        };
    }

    public EventEntity ConvertToUpdateEntity(HttpContext httpContext)
    {
        User contextUserData = httpContext.Items[NameConstants.USER_KEY] as User;

        return new EventEntity
        {
            PartitionKey = contextUserData.Id,
            RowKey = Id,
            Title = Title,
            Details = Details,
            StartDateTime = StartDateTime,
            EndDateTime = EndDateTime,
            Location = Location,
            Link = Link,
            ModifiedBy = contextUserData.Id
        };
    }
}
