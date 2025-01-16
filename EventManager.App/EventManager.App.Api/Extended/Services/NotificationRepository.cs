using Azure;
using Azure.Data.Tables;
using EventManager.App.Api.Basic.Models;
using EventManager.App.Api.Extended.Interfaces;
using EventManager.App.Api.Extended.Models;
using Microsoft.Extensions.Options;

namespace EventManager.App.Api.Extended.Services;

public class NotificationRepository : INotificationRepository
{
    private readonly TableClient tableClient;

    public NotificationRepository(IOptions<AzureTableConfig> azureTableConfig)
    {
        TableServiceClient tableServiceClient = new TableServiceClient(azureTableConfig.Value.ConnectionString);
        tableClient = tableServiceClient.GetTableClient(azureTableConfig.Value.NotificationsTable);
    }

    /// <inheritdoc/>
    public NotificationEntity GetNotification(string rowKey)
    {
        return tableClient.Query<NotificationEntity>(e => e.RowKey.Equals(rowKey, StringComparison.Ordinal)).FirstOrDefault();
    }

    /// <inheritdoc/>
    public List<NotificationEntity> GetNotifications()
    {
        return tableClient.Query<NotificationEntity>().OrderByDescending(e => e.Timestamp).ToList();
    }

    /// <inheritdoc/>
    public bool CreateNotification(NotificationEntity notificationEntity)
    {
        Response response = tableClient.AddEntity(notificationEntity);
        if (response.Status >= 200 && response.Status < 300)
        {
            return true;
        }
        return false;
    }

    /// <inheritdoc/>
    public bool UpdateNotification(NotificationEntity notificationEntity)
    {
        Response response = tableClient.UpdateEntity(notificationEntity, ETag.All, TableUpdateMode.Merge);
        if (response.Status >= 200 && response.Status < 300)
        {
            return true;
        }
        return false;
    }

    /// <inheritdoc/>
    public bool DeleteNotification(string partitionKey, string rowKey)
    {
        Response response = tableClient.DeleteEntity(partitionKey, rowKey);
        if (response.Status >= 200 && response.Status < 300)
        {
            return true;
        }
        return false;
    }
}
