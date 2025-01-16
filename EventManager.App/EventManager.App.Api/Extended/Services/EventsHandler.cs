using EventManager.App.Api.Basic.Constants;
using EventManager.App.Api.Basic.Models;
using EventManager.App.Api.Basic.Utilities;
using EventManager.App.Api.Extended.Interfaces;
using EventManager.App.Api.Extended.Models;
using System.Net;

namespace EventManager.App.Api.Extended.Services;

public class EventsHandler : IEventsHandler
{
    private readonly IEventsRepository eventRepository;
    private readonly ILogger<EventsHandler> logger;

    public EventsHandler(IEventsRepository eventRepository, ILogger<EventsHandler> logger)
    {
        this.eventRepository = eventRepository;
        this.logger = logger;
    }

    /// <inheritdoc/>
    public OpResult<List<EventData>> GetEvents(HttpContext httpContext)
    {
        logger.LogInformation($"{nameof(EventsHandler)}.{nameof(GetEvents)} => Method started for User: {ContextHelper.GetLoggedInUser(httpContext)?.Id}");
        OpResult<List<EventData>> opResult = new OpResult<List<EventData>>()
        {
            Status = HttpStatusCode.InternalServerError,
            ErrorCode = ErrorCode.Common_InternalServerError,
        };

        try
        {
            List<EventEntity> eventEntities = eventRepository.GetEvents();
            List<EventData> events = ConvertCollection(eventEntities);
            opResult.Result = events;
            opResult.Status = HttpStatusCode.OK;
            opResult.ErrorCode = ErrorCode.None;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, $"{nameof(EventsHandler)}.{nameof(GetEvents)} => Error occurred while fetching events for User: {ContextHelper.GetLoggedInUser(httpContext)?.Id}");
        }

        logger.LogInformation($"{nameof(EventsHandler)}.{nameof(GetEvents)} => Method completed for User: {ContextHelper.GetLoggedInUser(httpContext)?.Id}");
        return opResult;
    }

    /// <inheritdoc/>
    public OpResult<EventData> GetEvent(HttpContext httpContext, string eventId)
    {
        logger.LogInformation($"{nameof(EventsHandler)}.{nameof(GetEvent)} => Method started for User: {ContextHelper.GetLoggedInUser(httpContext)?.Id}");
        OpResult<EventData> opResult = new OpResult<EventData>()
        {
            Status = HttpStatusCode.InternalServerError,
            ErrorCode = ErrorCode.Common_InternalServerError,
        };

        try
        {
            if (!string.IsNullOrEmpty(eventId))
            {
                EventData eventData = (EventData)eventRepository.GetEvent(eventId);
                opResult.Result = eventData;
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
            logger.LogError(ex, $"{nameof(EventsHandler)}.{nameof(GetEvent)} => Error occurred while fetching event for User: {ContextHelper.GetLoggedInUser(httpContext)?.Id}");
        }

        logger.LogInformation($"{nameof(EventsHandler)}.{nameof(GetEvent)} => Method completed for User: {ContextHelper.GetLoggedInUser(httpContext)?.Id}");
        return opResult;
    }

    /// <inheritdoc/>
    public OpResult<EventData> CreateEvent(HttpContext httpContext, EventData rawEventData)
    {
        logger.LogInformation($"{nameof(EventsHandler)}.{nameof(CreateEvent)} => Method started for User: {ContextHelper.GetLoggedInUser(httpContext)?.Id}");
        OpResult<EventData> opResult = new OpResult<EventData>()
        {
            Status = HttpStatusCode.InternalServerError,
            ErrorCode = ErrorCode.Common_InternalServerError,
        };

        try
        {
            if (rawEventData is not null && rawEventData.IsValidToCreate() && httpContext is not null)
            {
                EventEntity eventEntity = rawEventData.ConvertToCreateEntity(httpContext);
                bool isSuccessful = eventRepository.CreateEvent(eventEntity);

                if (isSuccessful)
                {
                    opResult.Result = eventEntity;
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
            logger.LogError(ex, $"{nameof(EventsHandler)}.{nameof(CreateEvent)} => Error occurred while creating event for User: {ContextHelper.GetLoggedInUser(httpContext)?.Id}");
        }

        logger.LogInformation($"{nameof(EventsHandler)}.{nameof(CreateEvent)} => Method completed for User: {ContextHelper.GetLoggedInUser(httpContext)?.Id}");
        return opResult;
    }

    /// <inheritdoc/>
    public OpResult<EventData> UpdateEvent(HttpContext httpContext, EventData eventData)
    {
        logger.LogInformation($"{nameof(EventsHandler)}.{nameof(UpdateEvent)} => Method started for User: {ContextHelper.GetLoggedInUser(httpContext)?.Id}");
        OpResult<EventData> opResult = new OpResult<EventData>()
        {
            Status = HttpStatusCode.InternalServerError,
            ErrorCode = ErrorCode.Common_InternalServerError,
        };

        try
        {
            if (eventData is not null && eventData.IsValidToUpdate() && httpContext is not null)
            {
                EventEntity eventEntity = eventData.ConvertToUpdateEntity(httpContext);
                bool isSuccessful = eventRepository.UpdateEvent(eventEntity);

                if (isSuccessful)
                {
                    opResult.Result = eventEntity;
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
            logger.LogError(ex, $"{nameof(EventsHandler)}.{nameof(UpdateEvent)} => Error occurred while updating event for User: {ContextHelper.GetLoggedInUser(httpContext)?.Id}");
        }
        return opResult;
    }

    /// <inheritdoc/>
    public OpResult<bool> DeleteEvent(HttpContext httpContext, string eventId)
    {
        logger.LogInformation($"{nameof(EventsHandler)}.{nameof(DeleteEvent)} => Method started for User: {ContextHelper.GetLoggedInUser(httpContext)?.Id}");
        OpResult<bool> opResult = new OpResult<bool>()
        {
            Result = false,
            Status = HttpStatusCode.InternalServerError,
            ErrorCode = ErrorCode.Common_InternalServerError,
        };

        try
        {
            if (string.IsNullOrEmpty(eventId) && httpContext is not null)
            {
                UserEntity contextUserInfo = (UserEntity)httpContext.Items[NameConstants.USER_KEY];
                bool isSuccessful = eventRepository.DeleteEvent(contextUserInfo.RowKey, eventId);

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
            logger.LogError(ex, $"{nameof(EventsHandler)}.{nameof(DeleteEvent)} => Error occurred while deleting event for User: {ContextHelper.GetLoggedInUser(httpContext)?.Id}");
        }
        return opResult;
    }

    private List<EventData> ConvertCollection(List<EventEntity> eventEntities)
    {
        List<EventData> events = new List<EventData>();

        if (eventEntities is not null && eventEntities.Count > 0)
        {
            foreach (EventEntity eventEntity in eventEntities)
            {
                events.Add((EventData)eventEntity);
            }
        }

        return events;

    }
}
