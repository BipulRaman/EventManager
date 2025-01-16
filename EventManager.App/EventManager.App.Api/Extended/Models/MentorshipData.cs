using EventManager.App.Api.Basic.Constants;
using EventManager.App.Api.Basic.Models;
using System.Text.Json.Serialization;

namespace EventManager.App.Api.Extended.Models;

public class MentorshipData : BaseData
{
    [JsonPropertyName("subject")]
    public string Subject { get; set; }

    [JsonPropertyName("title")]
    public string Title { get; set; }

    [JsonPropertyName("message")]
    public string Message { get; set; }

    [JsonPropertyName("contact")]
    public string Contact { get; set; }

    public bool IsValidToCreate()
    {
        return !string.IsNullOrWhiteSpace(Subject)
            && !string.IsNullOrWhiteSpace(Title)
            && !string.IsNullOrWhiteSpace(Message)
            && !string.IsNullOrWhiteSpace(Contact);
    }

    public bool IsValidToUpdate()
    {
        return !string.IsNullOrWhiteSpace(Id)
            && !string.IsNullOrWhiteSpace(Subject)
            && !string.IsNullOrWhiteSpace(Title)
            && !string.IsNullOrWhiteSpace(Message)
            && !string.IsNullOrWhiteSpace(Contact);
    }

    public MentorshipEntity ConvertToCreateEntity(HttpContext httpContext)
    {
        User contextUserData = httpContext.Items[NameConstants.USER_KEY] as User;
        DateTime dateTime = DateTime.UtcNow;
        return new MentorshipEntity
        {
            PartitionKey = contextUserData.Id,
            RowKey = Guid.NewGuid().ToString(),
            Subject = Subject,
            Title = Title,
            Message = Message,
            Contact = Contact,
            CreatedAt = new DateTimeOffset(dateTime),
            Timestamp = new DateTimeOffset(dateTime),
            CreatedBy = contextUserData.Id,
            CreatedByName = contextUserData.Name,
            ModifiedBy = contextUserData.Id
        };
    }

    public MentorshipEntity ConvertToUpdateEntity(HttpContext httpContext)
    {
        User contextUserData = httpContext.Items[NameConstants.USER_KEY] as User;
        return new MentorshipEntity
        {
            PartitionKey = contextUserData.Id,
            RowKey = Id,
            Subject = Subject,
            Title = Title,
            Message = Message,
            Contact = Contact,
            Timestamp = ModifiedAt,
            ModifiedBy = contextUserData.Id
        };
    }
}
