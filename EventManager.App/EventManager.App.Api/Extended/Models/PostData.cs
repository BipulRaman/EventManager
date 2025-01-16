using EventManager.App.Api.Basic.Constants;
using EventManager.App.Api.Basic.Models;
using System.Text.Json.Serialization;

namespace EventManager.App.Api.Extended.Models;

public class PostData : BaseData
{
    [JsonPropertyName("title")]
    public string Title { get; set; }

    [JsonPropertyName("content")]
    public string Content { get; set; }

    [JsonPropertyName("image")]
    public string Image { get; set; }

    public bool IsValidToCreate()
    {
        return !string.IsNullOrWhiteSpace(Title)
            && !string.IsNullOrWhiteSpace(Content);
    }

    public bool IsValidToUpdate()
    {
        return !string.IsNullOrWhiteSpace(Id)
            && !string.IsNullOrWhiteSpace(Title)
            && !string.IsNullOrWhiteSpace(Content);
    }

    public PostEntity ConvertToCreateEntity(HttpContext httpContext)
    {
        User contextUserData = httpContext.Items[NameConstants.USER_KEY] as User;
        DateTime dateTime = DateTime.UtcNow;
        return new PostEntity
        {
            PartitionKey = contextUserData.Id,
            RowKey = Guid.NewGuid().ToString(),
            Title = Title,
            Content = Content,
            Image = Image,
            CreatedAt = new DateTimeOffset(dateTime),
            Timestamp = new DateTimeOffset(dateTime),
            CreatedBy = contextUserData.Id,
            CreatedByName = contextUserData.Name,
            ModifiedBy = contextUserData.Id
        };
    }

    public PostEntity ConvertToUpdateEntity(HttpContext httpContext)
    {
        User contextUserData = httpContext.Items[NameConstants.USER_KEY] as User;
        return new PostEntity
        {
            PartitionKey = contextUserData.Id,
            RowKey = Id,
            Title = Title,
            Content = Content,
            Image = Image,
            ModifiedBy = contextUserData.Id
        };
    }
}
