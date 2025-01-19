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

    [JsonPropertyName("date")]
    public DateTimeOffset Date { get; set; }

    public bool IsValidToCreate()
    {
        return !string.IsNullOrWhiteSpace(Title)
            && Amount != 0
            && Date != DateTimeOffset.MinValue;
    }

    public bool IsValidToUpdate()
    {
        return !string.IsNullOrWhiteSpace(Id)
            && !string.IsNullOrWhiteSpace(Title)
            && Amount != 0
            && Date != DateTimeOffset.MinValue;
    }

    public ExpenseEntity ConvertToCreateEntity(HttpContext httpContext)
    {
        User contextUserData = httpContext.Items[NameConstants.USER_KEY] as User;
        DateTime dateTime = DateTime.UtcNow;
        return new ExpenseEntity
        {
            PartitionKey = contextUserData.Id,
            RowKey = Guid.NewGuid().ToString(),
            Title = Title,
            Amount = Amount,
            Date = Date,
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
            Date = Date,
            ModifiedBy = contextUserData.Id
        };
    }
}
