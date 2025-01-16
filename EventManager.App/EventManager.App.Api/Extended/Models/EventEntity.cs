using EventManager.App.Api.Basic.Models;

namespace EventManager.App.Api.Extended.Models;

public class EventEntity : BaseEntity
{
    public string Title { get; set; }

    public string Details { get; set; }

    public DateTimeOffset StartDateTime { get; set; }

    public DateTimeOffset EndDateTime { get; set; }

    public string Location { get; set; }

    public string Link { get; set; }

    public static implicit operator EventData(EventEntity entity)
    {
        return new EventData
        {
            Id = entity.RowKey,
            Title = entity.Title,
            Details = entity.Details,
            StartDateTime = entity.StartDateTime,
            EndDateTime = entity.EndDateTime,
            Location = entity.Location,
            Link = entity.Link,
            CreatedAt = entity.CreatedAt,
            ModifiedAt = entity.Timestamp,
            CreatedByName = entity.CreatedByName,
            CreatedBy = entity.CreatedBy,
            ModifiedBy = entity.ModifiedBy,
        };
    }
}
