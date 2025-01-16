using EventManager.App.Api.Basic.Constants;
using EventManager.App.Api.Basic.Models;
using EventManager.App.Api.Basic.Utilities;
using EventManager.App.Api.Extended.Interfaces;
using EventManager.App.Api.Extended.Models;
using System.Net;

namespace EventManager.App.Api.Extended.Services;

public class NotificationHandler : INotificationHandler
{
    private readonly INotificationRepository notificationRepository;
    private readonly ILogger<NotificationHandler> logger;

    public NotificationHandler(INotificationRepository notificationRepository, ILogger<NotificationHandler> logger)
    {
        this.notificationRepository = notificationRepository;
        this.logger = logger;
    }

    /// <inheritdoc/>
    public OpResult<List<NotificationData>> GetNotifications(HttpContext httpContext)
    {
        logger.LogInformation($"{nameof(NotificationHandler)}.{nameof(GetNotifications)} => Method started for User: {ContextHelper.GetLoggedInUser(httpContext)?.Id}");
        OpResult<List<NotificationData>> opResult = new OpResult<List<NotificationData>>()
        {
            Status = HttpStatusCode.InternalServerError,
            ErrorCode = ErrorCode.Common_InternalServerError,
        };

        try
        {
            List<NotificationEntity> notificationEntities = notificationRepository.GetNotifications();
            List<NotificationData> notifications = ConvertCollection(notificationEntities);
            opResult.Result = notifications;
            opResult.Status = HttpStatusCode.OK;
            opResult.ErrorCode = ErrorCode.None;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, $"{nameof(NotificationHandler)}.{nameof(GetNotifications)} => Error occurred while fetching notifications for User: {ContextHelper.GetLoggedInUser(httpContext)?.Id}");
        }

        logger.LogInformation($"{nameof(NotificationHandler)}.{nameof(GetNotifications)} => Method completed for User: {ContextHelper.GetLoggedInUser(httpContext)?.Id}");
        return opResult;
    }

    /// <inheritdoc/>
    public OpResult<NotificationData> GetNotification(HttpContext httpContext, string notificationId)
    {
        logger.LogInformation($"{nameof(NotificationHandler)}.{nameof(GetNotification)} => Method started for User: {ContextHelper.GetLoggedInUser(httpContext)?.Id}");
        OpResult<NotificationData> opResult = new OpResult<NotificationData>()
        {
            Status = HttpStatusCode.InternalServerError,
            ErrorCode = ErrorCode.Common_InternalServerError,
        };

        try
        {
            if (!string.IsNullOrEmpty(notificationId))
            {
                NotificationData notificationData = (NotificationData)notificationRepository.GetNotification(notificationId);
                opResult.Result = notificationData;
                opResult.Status = HttpStatusCode.OK;
                opResult.ErrorCode = ErrorCode.None;
            }
            else
            {
                opResult.Status = HttpStatusCode.BadRequest;
                opResult.ErrorCode = ErrorCode.Common_BadRequest;
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, $"{nameof(NotificationHandler)}.{nameof(GetNotification)} => Error occurred while fetching notification for User: {ContextHelper.GetLoggedInUser(httpContext)?.Id}");
        }

        logger.LogInformation($"{nameof(NotificationHandler)}.{nameof(GetNotification)} => Method completed for User: {ContextHelper.GetLoggedInUser(httpContext)?.Id}");
        return opResult;
    }

    /// <inheritdoc/>
    public OpResult<NotificationData> CreateNotification(HttpContext httpContext, NotificationData rawNotificationData)
    {
        logger.LogInformation($"{nameof(NotificationHandler)}.{nameof(CreateNotification)} => Method started for User: {ContextHelper.GetLoggedInUser(httpContext)?.Id}");
        OpResult<NotificationData> opResult = new OpResult<NotificationData>()
        {
            Status = HttpStatusCode.InternalServerError,
            ErrorCode = ErrorCode.Common_InternalServerError,
        };

        try
        {
            if (rawNotificationData is not null && rawNotificationData.IsValidToCreate() && httpContext is not null)
            {
                NotificationEntity notificationEntity = rawNotificationData.ConvertToCreateEntity(httpContext);
                bool isSuccessful = notificationRepository.CreateNotification(notificationEntity);

                if (isSuccessful)
                {
                    opResult.Result = notificationEntity;
                    opResult.Status = HttpStatusCode.OK;
                    opResult.ErrorCode = ErrorCode.None;
                }
                else
                {
                    opResult.Status = HttpStatusCode.InternalServerError;
                    opResult.ErrorCode = ErrorCode.Entity_Create_Failed;
                }
            }
            else
            {
                opResult.Status = HttpStatusCode.BadRequest;
                opResult.ErrorCode = ErrorCode.Common_BadRequest;
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, $"{nameof(NotificationHandler)}.{nameof(CreateNotification)} => Error occurred while creating notification for User: {ContextHelper.GetLoggedInUser(httpContext)?.Id}");
        }

        logger.LogInformation($"{nameof(NotificationHandler)}.{nameof(CreateNotification)} => Method completed for User: {ContextHelper.GetLoggedInUser(httpContext)?.Id}");
        return opResult;
    }

    /// <inheritdoc/>
    public OpResult<NotificationData> UpdateNotification(HttpContext httpContext, NotificationData notificationData)
    {
        logger.LogInformation($"{nameof(NotificationHandler)}.{nameof(UpdateNotification)} => Method started for User: {ContextHelper.GetLoggedInUser(httpContext)?.Id}");
        OpResult<NotificationData> opResult = new OpResult<NotificationData>()
        {
            Status = HttpStatusCode.InternalServerError,
            ErrorCode = ErrorCode.Common_InternalServerError,
        };

        try
        {
            if (notificationData is not null && notificationData.IsValidToUpdate() && httpContext is not null)
            {
                NotificationEntity notificationEntity = notificationData.ConvertToUpdateEntity(httpContext);
                bool isSuccessful = notificationRepository.UpdateNotification(notificationEntity);

                if (isSuccessful)
                {
                    opResult.Result = notificationEntity;
                    opResult.Status = HttpStatusCode.OK;
                    opResult.ErrorCode = ErrorCode.None;
                }
                else
                {
                    opResult.Status = HttpStatusCode.InternalServerError;
                    opResult.ErrorCode = ErrorCode.Entity_Update_Failed;
                }
            }
            else
            {
                opResult.Status = HttpStatusCode.BadRequest;
                opResult.ErrorCode = ErrorCode.Common_BadRequest;
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, $"{nameof(NotificationHandler)}.{nameof(UpdateNotification)} => Error occurred while updating notification for User: {ContextHelper.GetLoggedInUser(httpContext)?.Id}");
        }
        return opResult;
    }

    /// <inheritdoc/>
    public OpResult<bool> DeleteNotification(HttpContext httpContext, string notificationId)
    {
        logger.LogInformation($"{nameof(NotificationHandler)}.{nameof(DeleteNotification)} => Method started for User: {ContextHelper.GetLoggedInUser(httpContext)?.Id}");
        OpResult<bool> opResult = new OpResult<bool>()
        {
            Result = false,
            Status = HttpStatusCode.InternalServerError,
            ErrorCode = ErrorCode.Common_InternalServerError,
        };

        try
        {
            if (string.IsNullOrEmpty(notificationId) && httpContext is not null)
            {
                UserEntity contextUserInfo = (UserEntity)httpContext.Items[NameConstants.USER_KEY];
                bool isSuccessful = notificationRepository.DeleteNotification(contextUserInfo.RowKey, notificationId);

                if (isSuccessful)
                {
                    opResult.Result = true;
                    opResult.Status = HttpStatusCode.OK;
                    opResult.ErrorCode = ErrorCode.None;
                }
                else
                {
                    opResult.Status = HttpStatusCode.InternalServerError;
                    opResult.ErrorCode = ErrorCode.Entity_Delete_Failed;
                }
            }
            else
            {
                opResult.Status = HttpStatusCode.BadRequest;
                opResult.ErrorCode = ErrorCode.Common_BadRequest;
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, $"{nameof(NotificationHandler)}.{nameof(DeleteNotification)} => Error occurred while deleting notification for User: {ContextHelper.GetLoggedInUser(httpContext)?.Id}");
        }
        return opResult;
    }

    private List<NotificationData> ConvertCollection(List<NotificationEntity> notificationEntities)
    {
        List<NotificationData> notifications = new List<NotificationData>();

        if (notificationEntities is not null && notificationEntities.Count > 0)
        {
            foreach (NotificationEntity notificationEntity in notificationEntities)
            {
                notifications.Add((NotificationData)notificationEntity);
            }
        }

        return notifications;

    }
}
