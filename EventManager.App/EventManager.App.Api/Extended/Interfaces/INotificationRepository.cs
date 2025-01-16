using EventManager.App.Api.Extended.Models;

namespace EventManager.App.Api.Extended.Interfaces;

public interface INotificationRepository
{
    /// <summary>
    /// Get notification by id.
    /// </summary>
    /// <param name="rowKey">Notification identity.</param>
    /// <returns></returns>
    NotificationEntity GetNotification(string rowKey);

    /// <summary>
    /// Get all notifications.
    /// </summary>
    /// <returns></returns>
    List<NotificationEntity> GetNotifications();

    /// <summary>
    /// Create notification.
    /// </summary>
    /// <param name="notificationEntity">Notification entity.</param>
    /// <returns></returns>
    bool CreateNotification(NotificationEntity notificationEntity);

    /// <summary>
    /// Update notification.
    /// </summary>
    /// <param name="notificationEntity">Notification entity.</param>
    /// <returns></returns>
    bool UpdateNotification(NotificationEntity notificationEntity);

    /// <summary>
    /// Delete notification.
    /// </summary>
    /// <param name="partitionKey">Notification partition key.</param>
    /// <param name="rowKey">Notification row key.</param>
    /// <returns></returns>
    bool DeleteNotification(string partitionKey, string rowKey);
}
