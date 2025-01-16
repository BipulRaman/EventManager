using Azure;
using Azure.Data.Tables;

namespace EventManager.App.Api.Basic.Models;

public class BaseEntity : ITableEntity
{
    public string PartitionKey { get; set; }

    public string RowKey { get; set; }

    public DateTimeOffset? Timestamp { get; set; }

    public string ModifiedBy { get; set; }

    public DateTimeOffset? CreatedAt { get; set; }

    public string CreatedBy { get; set; }

    public string CreatedByName { get; set; }

    ETag ITableEntity.ETag { get; set; }
}