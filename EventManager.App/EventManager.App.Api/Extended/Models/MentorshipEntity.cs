using EventManager.App.Api.Basic.Models;

namespace EventManager.App.Api.Extended.Models;

public class MentorshipEntity : BaseEntity
{
    public string Subject { get; set; }

    public string Title { get; set; }

    public string Message { get; set; }

    public string Contact { get; set; }

    public static implicit operator MentorshipData(MentorshipEntity entity)
    {
        return new MentorshipData
        {
            Id = entity.RowKey,
            Subject = entity.Subject,
            Title = entity.Title,
            Message = entity.Message,
            Contact = entity.Contact,
            ModifiedAt = entity.Timestamp,
            CreatedAt = entity.CreatedAt,
            CreatedBy = entity.CreatedBy,
            CreatedByName = entity.CreatedByName,
            ModifiedBy = entity.ModifiedBy,
        };
    }
}
