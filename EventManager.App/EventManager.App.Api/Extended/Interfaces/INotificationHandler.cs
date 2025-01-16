using EventManager.App.Api.Basic.Models;
using EventManager.App.Api.Extended.Models;

namespace EventManager.App.Api.Extended.Interfaces;

public interface INotificationHandler
{
    /// <summary>
    /// Get all notifications.
    /// </summary>
    /// <param name="httpContext">Context of the user.</param>
    /// <returns></returns>
    OpResult<List<NotificationData>> GetNotifications(HttpContext httpContext);

    /// <summary>
    /// Get notification by id.
    /// </summary>
    /// <param name="httpContext">Context of the user.</param>
    /// <param name="notificationId">Notification identity</param>
    /// <returns></returns>
    OpResult<NotificationData> GetNotification(HttpContext httpContext, string notificationId);

    /// <summary>
    /// Create notification.
    /// </summary>
    /// <param name="httpContext">Context of the user.</param>
    /// <param name="rawNotificationData">Raw notification creation entity.</param>
    /// <returns></returns>
    OpResult<NotificationData> CreateNotification(HttpContext httpContext, NotificationData rawNotificationData);

    /// <summary>
    /// Update notification.
    /// </summary>
    /// <param name="httpContext">Context of the user.</param>
    /// <param name="notificationData">Notification entity.</param>
    /// <returns></returns>
    OpResult<NotificationData> UpdateNotification(HttpContext httpContext, NotificationData notificationData);

    /// <summary>
    /// Delete notification.
    /// </summary>
    /// <param name="httpContext">Context of the user.</param>
    /// <param name="notificationId">Notification entity.</param>
    /// <returns></returns>
    OpResult<bool> DeleteNotification(HttpContext httpContext, string notificationId);
}
