using EventManager.App.Api.Basic.Models;

namespace EventManager.App.Api.Extended.Models;

public class ExpenseEntity : BaseEntity
{
    public string Title { get; set; }

    public float Amount { get; set; }
    public DateTimeOffset Date { get; set; }

    public static implicit operator ExpenseData(ExpenseEntity entity)
    {
        return new ExpenseData
        {
            Id = entity.RowKey,
            Title = entity.Title,
            Amount = entity.Amount,
            Date = entity.Date,
            CreatedAt = entity.CreatedAt,
            ModifiedAt = entity.Timestamp,
            CreatedBy = entity.CreatedBy,
            CreatedByName = entity.CreatedByName,
            ModifiedBy = entity.ModifiedBy
        };
    }
}
