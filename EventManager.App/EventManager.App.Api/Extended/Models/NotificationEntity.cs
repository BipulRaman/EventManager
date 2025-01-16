using EventManager.App.Api.Basic.Models;

namespace EventManager.App.Api.Extended.Models;

public class NotificationEntity : BaseEntity
{
    public string Title { get; set; }

    public string Message { get; set; }

    public static implicit operator NotificationData(NotificationEntity entity)
    {
        return new NotificationData
        {
            Id = entity.RowKey,
            Title = entity.Title,
            Message = entity.Message,
            CreatedAt = entity.CreatedAt,
            CreatedBy = entity.CreatedBy,
            CreatedByName = entity.CreatedByName,
            ModifiedAt = entity.Timestamp,
            ModifiedBy = entity.ModifiedBy
        };
    }
}
