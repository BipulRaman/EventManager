using EventManager.App.Api.Basic.Models;

namespace EventManager.App.Api.Extended.Models;

public class ExpenseEntity : BaseEntity
{
    public string Title { get; set; }

    public double Amount { get; set; }
    public DateTimeOffset DateTime { get; set; }

    public static implicit operator ExpenseData(ExpenseEntity entity)
    {
        return new ExpenseData
        {
            Id = entity.RowKey,
            Title = entity.Title,
            Amount = entity.Amount,
            DateTime = entity.DateTime,
            CreatedAt = entity.CreatedAt,
            ModifiedAt = entity.Timestamp,
            CreatedBy = entity.CreatedBy,
            CreatedByName = entity.CreatedByName,
            ModifiedBy = entity.ModifiedBy
        };
    }
}
