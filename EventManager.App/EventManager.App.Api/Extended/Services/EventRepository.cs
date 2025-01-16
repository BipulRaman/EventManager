using Azure;
using Azure.Data.Tables;
using EventManager.App.Api.Basic.Models;
using EventManager.App.Api.Extended.Interfaces;
using EventManager.App.Api.Extended.Models;
using Microsoft.Extensions.Options;

namespace EventManager.App.Api.Extended.Services;

public class EventRepository : IEventsRepository
{
    private readonly TableClient tableClient;

    public EventRepository(IOptions<AzureTableConfig> azureTableConfig)
    {
        TableServiceClient tableServiceClient = new TableServiceClient(azureTableConfig.Value.ConnectionString);
        tableClient = tableServiceClient.GetTableClient(azureTableConfig.Value.EventsTable);
    }

    /// <inheritdoc/>
    public EventEntity GetEvent(string rowKey)
    {
        return tableClient.Query<EventEntity>(e => e.RowKey.Equals(rowKey, StringComparison.Ordinal)).FirstOrDefault();
    }

    /// <inheritdoc/>
    public List<EventEntity> GetEvents()
    {
        return tableClient.Query<EventEntity>().OrderByDescending(e => e.Timestamp).ToList();
    }

    /// <inheritdoc/>
    public bool CreateEvent(EventEntity eventEntity)
    {
        Response response = tableClient.AddEntity(eventEntity);
        if (response.Status >= 200 && response.Status < 300)
        {
            return true;
        }
        return false;
    }

    /// <inheritdoc/>
    public bool UpdateEvent(EventEntity eventEntity)
    {
        Response response = tableClient.UpdateEntity(eventEntity, ETag.All, TableUpdateMode.Merge);
        if (response.Status >= 200 && response.Status < 300)
        {
            return true;
        }
        return false;
    }

    /// <inheritdoc/>
    public bool DeleteEvent(string partitionKey, string rowKey)
    {
        Response response = tableClient.DeleteEntity(partitionKey, rowKey);
        if (response.Status >= 200 && response.Status < 300)
        {
            return true;
        }
        return false;
    }
}
