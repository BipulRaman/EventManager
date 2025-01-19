using EventManager.App.Api.Basic.Constants;
using EventManager.App.Api.Basic.Models;
using System.Text.Json.Serialization;

namespace EventManager.App.Api.Extended.Models;

public class ExpenseData : BaseData
{
    [JsonPropertyName("title")]
    public string Title { get; set; }

    [JsonPropertyName("amount")]
    public float Amount { get; set; }

    [JsonPropertyName("dateTime")]
    public DateTimeOffset DateTime { get; set; }

    public bool IsValidToCreate()
    {
        return !string.IsNullOrWhiteSpace(Title)
            && Amount != 0
            && DateTime != DateTimeOffset.MinValue;
    }

    public bool IsValidToUpdate()
    {
        return !string.IsNullOrWhiteSpace(Id)
            && !string.IsNullOrWhiteSpace(Title)
            && Amount != 0
            && DateTime != DateTimeOffset.MinValue;
    }

    public ExpenseEntity ConvertToCreateEntity(HttpContext httpContext)
    {
        User contextUserData = httpContext.Items[NameConstants.USER_KEY] as User;
        DateTime dateTime = System.DateTime.UtcNow;
        return new ExpenseEntity
        {
            PartitionKey = contextUserData.Id,
            RowKey = Guid.NewGuid().ToString(),
            Title = Title,
            Amount = Amount,
            DateTime = DateTime,
            CreatedAt = new DateTimeOffset(dateTime),
            Timestamp = new DateTimeOffset(dateTime),
            CreatedBy = contextUserData.Id,
            CreatedByName = contextUserData.Name,
            ModifiedBy = contextUserData.Id
        };
    }

    public ExpenseEntity ConvertToUpdateEntity(HttpContext httpContext)
    {
        User contextUserData = httpContext.Items[NameConstants.USER_KEY] as User;
        return new ExpenseEntity
        {
            PartitionKey = contextUserData.Id,
            RowKey = Id,
            Title = Title,
            Amount = Amount,
            DateTime = DateTime,
            ModifiedBy = contextUserData.Id
        };
    }
}
