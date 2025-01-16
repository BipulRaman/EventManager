using EventManager.App.Api.Basic.Models;
using EventManager.App.Api.Extended.Models;

namespace EventManager.App.Api.Extended.Interfaces;

public interface IEventsHandler
{
    /// <summary>
    /// Get all events.
    /// </summary>
    /// <param name="httpContext">Context of the user.</param>
    /// <returns></returns>
    OpResult<List<EventData>> GetEvents(HttpContext httpContext);

    /// <summary>
    /// Get event by id.
    /// </summary>
    /// <param name="httpContext">Context of the user.</param>
    /// <param name="eventId">Event identity</param>
    /// <returns></returns>
    OpResult<EventData> GetEvent(HttpContext httpContext, string eventId);

    /// <summary>
    /// Create event.
    /// </summary>
    /// <param name="httpContext">Context of the user.</param>
    /// <param name="rawEventData">Raw event creation entity.</param>
    /// <returns></returns>
    OpResult<EventData> CreateEvent(HttpContext httpContext, EventData rawEventData);

    /// <summary>
    /// Update event.
    /// </summary>
    /// <param name="httpContext">Context of the user.</param>
    /// <param name="eventData">Event entity.</param>
    /// <returns></returns>
    OpResult<EventData> UpdateEvent(HttpContext httpContext, EventData eventData);

    /// <summary>
    /// Delete event.
    /// </summary>
    /// <param name="httpContext">Context of the user.</param>
    /// <param name="eventId">Event entity.</param>
    /// <returns></returns>
    OpResult<bool> DeleteEvent(HttpContext httpContext, string eventId);
}
