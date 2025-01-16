using EventManager.App.Api.Basic.Models;

namespace EventManager.App.Api.Extended.Models;

public class PostEntity : BaseEntity
{
    public bool Display { get; set; }

    public string Title { get; set; }

    public string Content { get; set; }

    public string Image { get; set; }

    public static implicit operator PostData(PostEntity entity)
    {
        return new PostData
        {
            Id = entity.RowKey,
            Title = entity.Title,
            Content = entity.Content,
            Image = entity.Image,
            CreatedAt = entity.CreatedAt,
            ModifiedAt = entity.Timestamp,
            CreatedBy = entity.CreatedBy,
            CreatedByName = entity.CreatedByName,
            ModifiedBy = entity.ModifiedBy
        };
    }
}
