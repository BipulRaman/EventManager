using EventManager.App.Api.Extended.Models;

namespace EventManager.App.Api.Extended.Interfaces;

public interface IEventsRepository
{
    /// <summary>
    /// Get event by id.
    /// </summary>
    /// <param name="rowKey">Event identity.</param>
    /// <returns></returns>
    EventEntity GetEvent(string rowKey);

    /// <summary>
    /// Get all events.
    /// </summary>
    /// <returns></returns>
    List<EventEntity> GetEvents();

    /// <summary>
    /// Create event.
    /// </summary>
    /// <param name="eventEntity">Event entity.</param>
    /// <returns></returns>
    bool CreateEvent(EventEntity eventEntity);

    /// <summary>
    /// Update event.
    /// </summary>
    /// <param name="eventEntity">Event entity.</param>
    /// <returns></returns>
    bool UpdateEvent(EventEntity eventEntity);

    /// <summary>
    /// Delete event.
    /// </summary>
    /// <param name="partitionKey">Event partition key.</param>
    /// <param name="rowKey">Event row key.</param>
    /// <returns></returns>
    bool DeleteEvent(string partitionKey, string rowKey);
}
